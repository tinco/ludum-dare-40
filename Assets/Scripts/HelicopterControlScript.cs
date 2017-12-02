using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterControlScript : MonoBehaviour {


    private float baseThrust = 50000;
    private float inputThrust = 40000;
    private float gravityForce = 1000;
    private Rigidbody rb;
	private HelicopterControls controls;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
		controls = GetComponent<HelicopterControls> ();
	}
	
	// Update is called once per frame
	void Update () {
		float currentThrust = baseThrust + controls.Vertical * inputThrust;
        rb.AddRelativeForce((
                Vector3.up * currentThrust 
                //- Vector3.up * rb.velocity.y * 0.01f
                ) 
            * Time.deltaTime);
        //rb.AddForce(-Vector3.up * gravityForce * Time.deltaTime);
	}
}
