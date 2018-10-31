using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArm : MonoBehaviour {

	// Use this for initialization
	public Vector3 inPos;
	public Vector3 outPos;
	
	// Update is called once per frame
	void Update () {
		var target = Input.GetButton("Fire1") ? outPos : inPos;
		transform.localPosition = Vector3.Lerp(transform.localPosition, target, 0.1f);
	}
}
