using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brush : MonoBehaviour
{

	AudioSource audioSource;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	void OnTriggerEnter(Collider other)
	{
		audioSource.Play();
	}
}
