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
    internal int foundPickups = 0;
    internal float completeTime = -1;
    internal bool isComplete = false;
    internal Pickupable[] initialpickups;
    internal bool isWrecked = false;

    internal GUIStyle guiStyle;
    internal float elapsedTime;
    internal bool isTimerActive;

    void Start()
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

        guiStyle = new GUIStyle();
        guiStyle.fontStyle = FontStyle.Bold;
        guiStyle.fontSize = 24;
        guiStyle.normal.textColor = Color.black;
        elapsedTime = 0;
        isTimerActive = true;
    }

    void OnDeath() {
		Deaths += 1;
        isWrecked = true;
        foreach (var pickup in initialpickups)
        {
            pickup.ForceDetach();
        }
        isTimerActive = false;
        var camera = GameObject.FindGameObjectWithTag("MainCamera");
        camera.GetComponent<CameraScript>().SetMobile(false);
    }

    void OnFinish()
    {

        isTimerActive = false;
        menuScript.TimeCardText.text = string.Format("Your time: {0}",GetFormattedTime());

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

    public virtual void OnRestart()
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
        elapsedTime = 0;
        isTimerActive = true;
    }

    public void Update()
    {
        if (Input.GetButtonDown("Cancel"))
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
    }

    public void FixedUpdate()
    {

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

        if (isTimerActive)
        {
            elapsedTime += Time.deltaTime;
        }
    }

    public virtual void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 250, 100), GetFormattedTime(), guiStyle);


        GUI.Label(new Rect(Screen.width - 160, 10, 250, 100), 
            string.Format("Pickups: {0} / {1}", foundPickups, requiredPickups), 
            guiStyle);

    }

    internal string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60F);
        int seconds = Mathf.FloorToInt(elapsedTime - minutes * 60F);
        int miliseconds = Mathf.FloorToInt((elapsedTime - minutes * 60F - seconds) * 60F);
        string niceTime = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, miliseconds);
        return niceTime;
    }
}