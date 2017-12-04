using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour {
	// This is set when this pickup is hooked by a player
	public HookBehaviour hook;
	public MagnetJoint magnet;
    public bool collected = false;

    private Vector3 startposition;
    private Quaternion startrotation;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        startposition = transform.position;
        startrotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
	}

    public void Reset()
    {
        ForceDetach();
        transform.position = startposition;
        transform.rotation = startrotation;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

    }

    public void ForceDetach()
    {
        BreakMagnet();
        if (magnet != null) {
            magnet.BreakJoint();
        }
        var joints = GetComponents<SpringJoint>();
        foreach(var joint in joints)
        {
            Destroy(joint);
        }
    }

    // This is called when we're hooked by a player
    public void Attach(HookBehaviour hook) {
		this.hook = hook;
	}

	public void Attract(MagnetJoint magnet) {
		this.magnet = magnet;
	}

	// This class is responsible for telling the hooking player that the joint broke.
	public void OnJointBreak(float breakForce)
	{
        if (hook != null)
        {
            this.hook.Detach(this);
        }
	}

	public void BreakMagnet() {
        if (magnet != null)
        {
            magnet.BreakJoint();
        }
	}
}
