using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishBehaviour : MonoBehaviour {
	void OnTriggerEnter()
	{
		FindObjectOfType<GameController> ().BroadcastMessage("OnFinish");
	}
}
