using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class GameController : MonoBehaviour {
	public int Deaths = 0;
	public HelicopterControlScript helicopterInstance;
    public GameObject helicopterPrefab;
    public LevelMenuScript menuScript;

    public int requiredPickups = 0;
    private int foundPickups = 0;
    private float completeTime = -1;
    private bool isComplete = false;
    private Pickupable[] initialpickups;
    private bool isWrecked = false;
    

    private void Start()
    {
        initialpickups = (Pickupable[])FindObjectsOfType(typeof(Pickupable));
        if(initialpickups == null || initialpickups.Length == 0)
        {
            throw new MissingComponentException("No pickups in level");
        }
        if(requiredPickups == 0)
        {
            requiredPickups = initialpickups.Length;
        }
    }

    void OnDeath() {
		Deaths += 1;
        isWrecked = true;
        foreach (var pickup in initialpickups)
        {
            pickup.ForceDetach();
        }

        var camera = GameObject.FindGameObjectWithTag("MainCamera");
        camera.GetComponent<CameraScript>().SetMobile(false);
    }

    void OnFinish()
    {
        helicopterInstance.SetImmortal(true);
        isComplete = true;
        completeTime = 1;

        var camera = GameObject.FindGameObjectWithTag("MainCamera");
        camera.GetComponent<CameraScript>().SetMobile(false);
    }

    public void OnPickupCollected()
    {
        foundPickups++;
        if(foundPickups >= requiredPickups && !isWrecked)
        {
            OnFinish();
        }

    }

    public void OnRestart()
    {
        isComplete = false;
        isWrecked = false;
        foundPickups = 0;

        var spawn = GameObject.FindGameObjectWithTag("Respawn");

        //helicopterInstance.Reset(spawn);
        Destroy(helicopterInstance.gameObject);
        var playerInstance = Instantiate(helicopterPrefab, spawn.transform.position, spawn.transform.rotation);
        helicopterInstance = playerInstance.GetComponent<HelicopterControlScript>();

        var camera = GameObject.FindGameObjectWithTag("MainCamera");
        camera.GetComponent<CameraScript>().Helicopter = playerInstance.GetComponent<Rigidbody>();
        camera.GetComponent<CameraScript>().SetMobile(true);

        //Pickupable[] pickups = (Pickupable[]) FindObjectsOfType(typeof(Pickupable));
        Debug.Log("Pickups:" + initialpickups.Length);
        foreach(var pickup in initialpickups)
        {
            pickup.gameObject.SetActive(true);
            pickup.Reset();
        }
        Time.timeScale = 1;
    }

    public void FixedUpdate()
    {
        if(Input.GetAxis("Cancel") > 0.5)
        {
            if (!menuScript.PauseMenu.activeSelf)
            {
                Time.timeScale = 0;
                menuScript.PauseMenu.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                menuScript.PauseMenu.SetActive(false);
            }
        }

        if (isComplete)
        {
            if(completeTime > 0)
            {
                completeTime -= Time.deltaTime;
            }
            else
            {
                menuScript.LevelCompleteMenu.SetActive(true);
            }
        }
    }
}