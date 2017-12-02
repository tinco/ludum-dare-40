using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Rigidbody Helicopter;

    private Vector3 relativeTargetPosition;
	// Use this for initialization
	void Start () {
        relativeTargetPosition = this.transform.position - Helicopter.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 absoluteTargetPosition = Helicopter.transform.position + relativeTargetPosition;
        this.transform.position = absoluteTargetPosition;
        this.transform.LookAt(Helicopter.transform);
        var a = this.transform;
 
	}
}
