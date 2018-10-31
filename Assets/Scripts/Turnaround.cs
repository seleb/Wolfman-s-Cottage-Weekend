using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Turnaround : MonoBehaviour
{
	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (hit.gameObject.tag == "Turnaround")
		{
			// hacky and incorrect math but it works
			var v = Vector3.Angle(transform.forward, -transform.position);
			GetComponent<MouseLook>().rotationX += v;
		}
	}
}
