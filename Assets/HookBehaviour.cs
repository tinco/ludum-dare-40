﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookBehaviour : MonoBehaviour {
	public List<Pickupable> AttachedPickups;

	public float BreakForce = 100;
	public float BreakTorque = 100;
	public float JointDamper = 1;
	public float JointForce = 1;

	void OnCollisionEnter(Collision collision)
	{
		foreach (ContactPoint contact in collision.contacts)
		{
			var otherBody = contact.otherCollider.attachedRigidbody;
			var pickupable = otherBody ? otherBody.gameObject.GetComponent<Pickupable> () : null;
			if (pickupable != null && !AttachedPickups.Contains(pickupable)) {
				Attach (pickupable, contact);
			}
		}
	}

	void Attach(Pickupable pickup, ContactPoint contact) {
		AttachedPickups.Add (pickup);
		CreatePickupJoint (pickup.gameObject, contact);
		pickup.Attach (this);
	}

	// This method is called by the pickup when the joint breaks.
	public void Detach(Pickupable pickup) {
		Debug.Log ("Pickup detached!");
		AttachedPickups.Remove (pickup);
	}

	void CreatePickupJoint(GameObject pickup, ContactPoint contact) {
		var joint = pickup.AddComponent<SpringJoint> ();
		joint.connectedBody = gameObject.GetComponent<Rigidbody> ();
		joint.breakForce = BreakForce;
		joint.breakTorque = BreakTorque;
		joint.enableCollision = true;
		joint.damper = JointDamper;
		joint.spring = JointForce;
		joint.maxDistance = 0.05f;
	}
}