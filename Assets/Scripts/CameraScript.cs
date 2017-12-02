using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Rigidbody Helicopter;
    public float damping = 1;
    public float cameraMotion = 1F;
    private Vector3 baseRelativeTargetPosition;

	// Use this for initialization
	void Start () {
        baseRelativeTargetPosition = new Vector3 (0, 8, -10);
        
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        var localVelocity = Helicopter.transform.InverseTransformDirection(Helicopter.velocity);
        Vector3 currentRelativeTargetPosition = baseRelativeTargetPosition 
            + cameraMotion * new Vector3(0 , 0, -localVelocity.z);

        float currentAngle = transform.eulerAngles.y;
        float targetAngle = Helicopter.transform.eulerAngles.y;
        float angle = Mathf.LerpAngle(targetAngle, currentAngle, Time.deltaTime * damping);

        Quaternion rotate = Quaternion.Euler(0, angle, 0);
        transform.position = Helicopter.transform.position + (rotate * currentRelativeTargetPosition);

        //Vector3 absoluteTargetPosition = Helicopter.transform.position + relativeTargetPosition;
        //this.transform.position = absoluteTargetPosition;
        transform.LookAt(Helicopter.transform);


    }
}
