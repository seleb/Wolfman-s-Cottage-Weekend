using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class StartFire : MonoBehaviour
{

	public GameObject stick;
	// Update is called once per frame
	private ParticleSystem psFire;
	private ParticleSystem psStick;
	private PostProcessVolume ppvFire;
	private PostProcessVolume ppvStick;
	void Start(){
			psFire = GetComponent<ParticleSystem>();
			psStick = stick.GetComponentInChildren<ParticleSystem>();
			ppvFire = GetComponent<PostProcessVolume>();
			ppvStick = stick.GetComponentInChildren<PostProcessVolume>();
	}
	void Update()
	{
		if (Vector3.Distance(stick.transform.position, transform.position) < 1f)
		{
			if(!psFire.isPlaying){
				psFire.Play();
			}
			if(!psStick.isPlaying){
				psStick.Play();
			}
			ppvFire.enabled = true;
			ppvStick.enabled = true;
		}
	}
}
