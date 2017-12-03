using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetJoint : MonoBehaviour {
	public Rigidbody ConnectedBody;

	public float MagnetForce = 2000;

	// Distance until which a force is applied, after which the joint is broken
	public float MaxDistance = 10f;

	// Distance until which no force is applied
	public float MinDistance = 0.5f;

	public bool Broken = false;
	private bool colliding = true;

	private Rigidbody thisBody;
	private Collider thisCollider;

	// Use this for initialization
	void Start () {
		thisBody = gameObject.GetComponent<Rigidbody> ();
		thisCollider = thisBody.GetComponent<Collider> ();
		if (ConnectedBody == null) {
			throw new Exception ("Must set ConnectedBody on magnet joint");
		}
		Debug.Log ("MagnetJoint established");
	}

	// Update is called once per frame
	void Update () {
		var difference =  thisBody.position - ConnectedBody.position;
		var distance = difference.magnitude;


		if (distance > MaxDistance) {
			breakJoint ();
		} else if (!colliding) {
			var direction = difference.normalized;
			var force = (direction * ( 1 / (distance*distance)) * MagnetForce);
			// ConnectedBody.AddForce (force);
			thisBody.AddForce (-force);
			//Debug.Log (-force);
		}
	}

	void breakJoint() {
		//TODO
		Debug.Log("MagnetJoint should be broken");
		Broken = true;
	}

	void OnCollisionEnter(Collision collision) {
		foreach (ContactPoint contact in collision.contacts) {
			if (contact.otherCollider.gameObject == ConnectedBody) {
				colliding = true;
				Debug.Log ("Colliding");
				break;
			} else {
				Debug.Log ("Colliding with: " + contact.otherCollider.gameObject.name);
			}
		}
	}

	void OnCollisionExit(Collision collision) {
		foreach (ContactPoint contact in collision.contacts) {
			if (contact.otherCollider == ConnectedBody) {
				colliding = false;
				Debug.Log ("Not colliding");
				break;
			}
		}
	}

}
