using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour {
	// This is set when this pickup is hooked by a player
	public HookBehaviour hook;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// This is called when we're hooked by a player
	public void Attach(HookBehaviour hook) {
		this.hook = hook;
	}

	// This class is responsible for telling the hooking player that the joint broke.
	void OnJointBreak(float breakForce)
	{
		this.hook.Detach (this);
	}
}
