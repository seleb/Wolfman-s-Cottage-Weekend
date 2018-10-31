using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arms : MonoBehaviour
{

	// Use this for initialization
	private FirstPersonDrifter drifter;
	private Camera cam;
	public GameObject container;
	private List<Transform> meshes = new List<Transform>();
	private List<Vector3> init = new List<Vector3>();
	private List<Transform> armUp = new List<Transform>();
	private List<Transform> armDown = new List<Transform>();
	private List<Transform> armOut = new List<Transform>();
	void Start()
	{
		drifter = GetComponent<FirstPersonDrifter>();
		cam = GetComponentInChildren<Camera>();
		for(var i = 0; i < container.transform.childCount; ++i){
			var arm = container.transform.GetChild(i);
			init.Add(arm.transform.eulerAngles);
			meshes.Add(arm.transform.Find("mesh"));
			armUp.Add(arm.transform.Find("up"));
			armDown.Add(arm.transform.Find("down"));
			armOut.Add(arm.transform.Find("out"));
		}
	}

	// Update is called once per frame
	void Update()
	{
		for(var i = 0; i < meshes.Count; ++i){
			var mesh = meshes[i];
			Transform target = armOut[i];
			if (drifter.isFalling())
			{
				target = armUp[i];
			}
			else if (drifter.isJumping())
			{
				target = armDown[i];
			}
			Quaternion a = mesh.transform.rotation;
			mesh.transform.LookAt(target);
			mesh.transform.rotation = Quaternion.Slerp(a, mesh.transform.rotation, 0.03f);
		}
		container.transform.rotation = Quaternion.Slerp(container.transform.rotation, cam.transform.rotation, 0.5f);
	}

	public void jump()
	{
		// armRight.transform.Rotate(15.0f, 0, 0);
		// armLeft.transform.Rotate(15.0f, 0, 0);
	}
}
