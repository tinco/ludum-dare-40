using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBehaviour : MonoBehaviour {
	public GameObject Wreck;

	void OnRotorCollision () {
		FindObjectOfType<GameController> ().BroadcastMessage("OnDeath");
		Instantiate(Wreck, transform.position, transform.rotation);
		Destroy (gameObject);
	}
}
