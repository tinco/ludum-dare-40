using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterControls : MonoBehaviour {
	public float Vertical;
	public float Horizontal;

	// Use this for initialization
	void Start () {
		Vertical = 0;
		Horizontal = 0;
	}
	
	// Update is called once per frame
	void Update () {
		Vertical = Input.GetAxis ("Vertical");
		Horizontal = Input.GetAxis ("Horizontal");
	}
}
