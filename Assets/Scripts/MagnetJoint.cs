using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetJoint : MonoBehaviour {
	public Rigidbody ConnectedBody;

	public float MagnetForce = 500;

	// Distance until which a force is applied, after which the joint is broken
	public float MaxDistance = 5f;

	// Distance until which no force is applied
	public float MinDistance = 1f;

	public bool Broken = false;

	private Rigidbody thisBody;
	private Collider thisCollider;

	// Use this for initialization
	void Start () {
		thisBody = gameObject.GetComponent<Rigidbody> ();
		thisCollider = thisBody.GetComponent<Collider> ();
		if (ConnectedBody == null) {
			throw new Exception ("Must set ConnectedBody on magnet joint");
		}
	}

	// Update is called once per frame
	void Update () {
		var difference =  thisBody.position - ConnectedBody.position;
		var distance = difference.magnitude;
		if (distance > MaxDistance) {
			BreakJoint ();
		} else if (distance > MinDistance) {
			var direction = difference.normalized;
			var force = (direction * ( 1 / distance) * MagnetForce);
			thisBody.AddForce (-force);
		}
	}

	public void BreakJoint() {
		Broken = true;
		Destroy (this);
	}


}
