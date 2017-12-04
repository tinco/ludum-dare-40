using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotorRotation : MonoBehaviour {
	public enum OrientationType { Horizontal, Vertical }
	public float RotationSpeed;
	public OrientationType Orientation;

	// Update is called once per frame
	void Update () {
		var orientationVector = Orientation == OrientationType.Horizontal ? new Vector3 (0, 0, 1) : new Vector3 (0, 1, 0);
		gameObject.transform.Rotate (orientationVector * RotationSpeed);
	}
}
