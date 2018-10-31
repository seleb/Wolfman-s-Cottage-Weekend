using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class FireStick : Examinable
{
	public override void OnDrop()
	{
		base.OnDrop();
		GetComponentInChildren<ParticleSystem>().Stop();
		GetComponentInChildren<PostProcessVolume>().enabled = false;
	}
}
