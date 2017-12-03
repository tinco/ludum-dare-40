using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public int Deaths = 0;
	public GameObject Player;

	void OnDeath() {
		Deaths += 1;
		var spawn = GameObject.FindGameObjectWithTag ("Respawn");
		var playerInstance = Instantiate(Player, spawn.transform.position, spawn.transform.rotation);
		var camera = GameObject.FindGameObjectWithTag ("MainCamera");
		camera.GetComponent<CameraScript> ().Helicopter = playerInstance.GetComponent<Rigidbody>();
	}
}