using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotorCollisions : MonoBehaviour {
	void OnTriggerEnter(Collider other)
	{
        if (!other.isTrigger){
            gameObject.SendMessageUpwards("OnRotorCollision");
        }
	}
}
