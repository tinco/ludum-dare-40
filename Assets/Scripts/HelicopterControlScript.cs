﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterControlScript : MonoBehaviour {


    private float baseThrust = 1000;
    private float inputThrust = 600;
    public float inputPitch = 25F;
    public float inputRoll = 50F;
    public float inputYaw = 30F;
    private float gravityForce = 1000;

    public float forwardDrag = 40;
    public float upwardDrag = 50;
    public float sidewaysDrag = 40;

    public float pitchDrag = 20F;
    public float rollDrag = 50F;
    public float yawDrag = 35F;
    //private float pitchDrag = 140F;
    //private float rollDrag = 180F;
    //private float yawDrag = 100F;

    public float pitchRecovery = 15F;
    public float rollRecovery = 40F;

    private Rigidbody rb;
	private HelicopterControls controls;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
		controls = GetComponent<HelicopterControls> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float currentThrust = baseThrust + controls.Thrust * inputThrust;

        Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
        Vector3 localAngularVelocity = transform.InverseTransformDirection(rb.angularVelocity);



        rb.AddRelativeTorque(new Vector3(
            controls.Pitch * inputPitch
                - localAngularVelocity.x * pitchDrag
                - Mathf.Tan(rb.transform.localEulerAngles.x * Mathf.PI / 180) * pitchRecovery,
            controls.Yaw * inputYaw
                - localAngularVelocity.y * yawDrag,
            controls.Roll * inputRoll
                - localAngularVelocity.z * rollDrag
                - Mathf.Tan(rb.transform.localEulerAngles.z * Mathf.PI / 180) * rollRecovery
            ) * Time.deltaTime , ForceMode.Impulse);

        rb.AddRelativeForce(
            new Vector3(
                -localVelocity.x * forwardDrag,
                currentThrust - localVelocity.y * upwardDrag,
                -localVelocity.z * sidewaysDrag
                ) * Time.deltaTime,
            ForceMode.Impulse);
        rb.AddForce(-Vector3.up * gravityForce * Time.deltaTime, ForceMode.Impulse);
	}
}
