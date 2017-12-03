using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishBehaviour : MonoBehaviour {

    public int requiredPickups = 1;

	void OnTriggerEnter(Collision other)
	{
        if (other.gameObject.CompareTag("Pickup"))
        {
            FindObjectOfType<GameController>().BroadcastMessage("OnFinish");
        }

	}
}
