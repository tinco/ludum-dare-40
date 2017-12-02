using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Rigidbody Helicopter;
    public float damping = 1;

    private Vector3 relativeTargetPosition;
	// Use this for initialization
	void Start () {
        relativeTargetPosition = this.transform.position - Helicopter.transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float currentAngle = transform.eulerAngles.y;
        float targetAngle = Helicopter.transform.eulerAngles.y;
        float angle = Mathf.LerpAngle(targetAngle, currentAngle, Time.deltaTime * damping);
        Debug.Log(currentAngle + "   " + targetAngle + "  " +angle + "  |  " + (targetAngle - angle) + "  " + Time.deltaTime * damping);
        Quaternion rotate = Quaternion.Euler(0, angle, 0);
        transform.position = Helicopter.transform.position + (rotate * relativeTargetPosition);

        //Vector3 absoluteTargetPosition = Helicopter.transform.position + relativeTargetPosition;
        //this.transform.position = absoluteTargetPosition;
        transform.LookAt(Helicopter.transform);

 
	}
}
