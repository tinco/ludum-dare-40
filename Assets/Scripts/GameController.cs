using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class GameController : MonoBehaviour {
	public int Deaths = 0;
	public GameObject Player;
    public GameObject PauseMenu;


    void OnDeath() {
		Deaths += 1;
        var spawn = GameObject.FindGameObjectWithTag("Respawn");
        var playerInstance = Instantiate(Player, spawn.transform.position, spawn.transform.rotation);
        var camera = GameObject.FindGameObjectWithTag("MainCamera");
        camera.GetComponent<CameraScript>().Helicopter = playerInstance.GetComponent<Rigidbody>();
        Time.timeScale = 1;
    }

    public void OnRestart()
    {
        Debug.Log("restart");
        var spawn = GameObject.FindGameObjectWithTag("Respawn");
        Player.transform.position = spawn.transform.position;
        Player.transform.rotation = spawn.transform.rotation;
        var rb = Player.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        Time.timeScale = 1;
    }

    public void FixedUpdate()
    {
        if(Input.GetAxis("Cancel") > 0.5)
        {
            if (!PauseMenu.activeSelf)
            {
                Time.timeScale = 0;
                PauseMenu.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                PauseMenu.SetActive(false);
            }
        }
    }
}