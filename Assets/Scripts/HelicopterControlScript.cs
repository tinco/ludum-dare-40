using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterControlScript : MonoBehaviour {


    public float baseThrust = 10000;
    public float inputThrust = 6000;
    public float inputPitch = 40F;
    public float inputRoll = 50F;
    public float inputYaw = 30F;

    public float forwardDrag = 40;
    public float upwardDrag = 50;
    public float sidewaysDrag = 40;

    public float pitchDrag = 40F;
    public float rollDrag = 50F;
    public float yawDrag = 35F;

    public float pitchRecovery = 8F;
    public float rollRecovery = 40F;

    public float forwardSpeedBoostFraction = 3F;

    private Rigidbody rb;
	private HelicopterControls controls;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
		controls = GetComponent<HelicopterControls> ();
	}

    // Update is called once per frame
    void FixedUpdate()
    {
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
            ), ForceMode.Force);

        rb.AddRelativeForce(
            new Vector3(
                - localVelocity.x * sidewaysDrag,
                currentThrust - localVelocity.y * upwardDrag,
                Mathf.Max(new float[] { localVelocity.magnitude - 10, 0 }) * forwardSpeedBoostFraction - localVelocity.z * forwardDrag
                ),
            ForceMode.Force);


        //rb.AddForce(-Vector3.up * gravityForce * Time.deltaTime, ForceMode.Impulse);
    }
}
