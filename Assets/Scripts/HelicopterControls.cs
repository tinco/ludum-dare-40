using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterControls : MonoBehaviour {
	public float Thrust;
	public float Yaw;
    public float Pitch;
    public float Roll;
	// Use this for initialization
	void Start () {
        Thrust = 0;
        Yaw = 0;
	}
	
	// Update is called once per frame
	void Update () {
        Thrust = Input.GetAxis ("Thrust");
        Yaw = Input.GetAxis ("Yaw");
        Pitch = Input.GetAxis("Pitch");
        Roll = Input.GetAxis("Roll");
	}
}
