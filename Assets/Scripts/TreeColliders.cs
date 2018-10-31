using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeColliders : MonoBehaviour
{
	ParticleSystem ps;
	// Use this for initialization

	void Start()
	{
		ps = GetComponent<ParticleSystem>();
		StartCoroutine(LateStart(0.01f));
	}

	IEnumerator LateStart(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		ParticleSystem.Particle[] parts = new ParticleSystem.Particle[ps.main.maxParticles];
		// GetParticles is allocation free because we reuse the parts buffer between updates
		int numParticlesAlive = ps.GetParticles(parts);

		// Change only the particles that are alive
		for (int i = 0; i < numParticlesAlive; i++)
		{
			var objToSpawn = new GameObject("treeCollider");
			objToSpawn.isStatic = true;
			objToSpawn.transform.position = parts[i].position;
			objToSpawn.transform.parent = transform;
			//Add Components
			CapsuleCollider c = objToSpawn.AddComponent<CapsuleCollider>();
			Vector3 size = parts[i].startSize3D;
			c.radius = (size.x + size.z) / 2.0f;
			c.height = size.y * 4f;
			c.center = new Vector3(0, c.height / 2f, 0);
		}
	}
}
