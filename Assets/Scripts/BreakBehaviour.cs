using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBehaviour : MonoBehaviour {
	public GameObject Wreck;
    private GameObject wreckInstance;
    private float wreckTime = -1;
    private bool isWrecked = false;

	void OnRotorCollision () {
        Debug.Log("Foute Boem!");
        wreckInstance = Instantiate(Wreck, transform.position, transform.rotation);
        wreckTime = 2;
        isWrecked = true;
        FindObjectOfType<GameController>().BroadcastMessage("OnDeath");
    }

    private void FixedUpdate()
    {
        if (isWrecked)
        {
            if (wreckTime > 0)
            {
                wreckTime -= Time.deltaTime;
            }
            else
            {
                FindObjectOfType<GameController>().BroadcastMessage("OnRestart");
                GameObject.Destroy(wreckInstance);
                isWrecked = false;
            }
        }
    }
}
