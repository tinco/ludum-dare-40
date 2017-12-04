using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempRotationScript : MonoBehaviour {


	// Update is called once per frame
	void FixedUpdate () {
        transform.Rotate(new Vector3(1, 0, 0), 900 * Time.deltaTime, Space.Self);
	}
}
