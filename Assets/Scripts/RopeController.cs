using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode()]
public class RopeController : MonoBehaviour {
	public List<Transform> Positions;

	public LineRenderer LineRenderer;
	
	// Update is called once per frame
	void Update () {
		LineRenderer.positionCount = Positions.Count;
		int i = 0;
		foreach (var position in Positions) {
			LineRenderer.SetPosition (i, position.position);
			i += 1;
		}
	}
}
