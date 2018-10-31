using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Examinable : MonoBehaviour
{
	public string titleText = "";
	[TextArea(3, 10)]
	public string descriptionText = "";
	public Material outlineMat;
	private GameObject outline;

	void Start()
	{
		outline = new GameObject("outline");
		outline.transform.parent = transform;
		outline.transform.localScale = Vector3.one;
		outline.transform.localEulerAngles = Vector3.zero;
		outline.transform.localPosition = Vector3.zero;
		var mesh = outline.AddComponent<MeshFilter>();
		mesh.mesh = GetComponent<MeshFilter>().mesh;
		var ren = outline.AddComponent<MeshRenderer>();
		Material[] materials = new Material[GetComponent<MeshRenderer>().materials.Length];
		for (var i = 0; i < materials.Length; ++i)
		{
			materials[i] = outlineMat;
		}
		ren.materials = materials;
		outline.SetActive(false);
	}

	public void OnHover()
	{
		outline.SetActive(true);
	}

	public void OnBlur()
	{
		outline.SetActive(false);
	}
	public void OnPickup()
	{
		outline.SetActive(false);
	}

	public virtual void OnDrop()
	{
	}

	public void OnExamine()
	{
	}
}
