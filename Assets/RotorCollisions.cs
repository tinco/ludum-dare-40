using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotorCollisions : MonoBehaviour {
	void OnTriggerEnter()
	{
		gameObject.SendMessageUpwards ("OnRotorCollision");
	}
}
