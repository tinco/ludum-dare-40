using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishBehaviour : MonoBehaviour {

    //public float BreakForce = 100;
    //public float BreakTorque = 100;
    //public float JointDamper = 1;
    //public float JointForce = 1;

    void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.CompareTag("Pickup"))
        {
            var pickupable = other.gameObject.GetComponent<Pickupable>();
            pickupable.ForceDetach();
            pickupable.gameObject.SetActive(false);
            FindObjectOfType<GameController>().BroadcastMessage("OnPickupCollected");
        }
        

	}

    //void CreatePickupJoint(GameObject pickup)
    //{
    //    var joint = pickup.AddComponent<SpringJoint>();
    //    joint.connectedBody = gameObject.GetComponent<Rigidbody>();
    //    joint.breakForce = BreakForce;
    //    joint.breakTorque = BreakTorque;
    //    joint.enableCollision = true;
    //    joint.damper = JointDamper;
    //    joint.spring = JointForce;
    //    joint.maxDistance = 0.05f;
    //}
}
