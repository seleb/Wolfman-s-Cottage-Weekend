using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickuper : MonoBehaviour
{

	public string button;
	private Transform hold;
	private GameObject hover;
	public GameObject held;
	private Vector3 vel;
	private Vector3 lastPos;
	private AudioSource pickupSound;
	// Use this for initialization
	void Start()
	{
		hold = transform.Find("hold");
		pickupSound = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{
		RaycastHit hit;
		Ray rayFromPlayer = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
		GameObject newHover = null;
		if (Physics.Raycast(rayFromPlayer, out hit, 2, 1 << LayerMask.NameToLayer("Pickup")))

		{
			newHover = hit.collider.gameObject;

			if (Input.GetButtonDown(button))
			{
				held = hit.collider.gameObject;
				var e = held.GetComponent<Examinable>();
				if (e)
				{
					e.OnPickup();
					pickupSound.pitch = Random.Range(0.8f, 1.2f);
					pickupSound.Play();
				}
				held.GetComponent<Collider>().enabled = false;
				held.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
				var collider = held.GetComponent<SphereCollider>();
				float r;
				if (collider)
				{
					r = collider.radius;
				}
				else
				{
					r = held.GetComponent<CapsuleCollider>().radius;
				}
				lastPos = held.transform.position = hold.position + rayFromPlayer.direction * r * held.transform.lossyScale.x;
				held.transform.parent = hold.transform;
				held.transform.localRotation = Quaternion.Euler(Vector3.zero);
			}
		}

		if (newHover != hover)
		{
			if (hover)
			{
				var e = hover.GetComponent<Examinable>();
				if (e)
				{
					e.OnBlur();
				}
			}
			hover = newHover;
			if (hover)
			{
				var e = hover.GetComponent<Examinable>();
				if (e)
				{
					e.OnHover();
				}
			}
		}

		if (held)
		{
			if (Input.GetButtonUp(button))
			{
				var e = held.GetComponent<Examinable>();
				if (e)
				{
					e.OnDrop();
				}
				held.GetComponent<Collider>().enabled = true;
				var rb = held.GetComponent<Rigidbody>();
				held.transform.parent = null;
				held = null;
				rb.constraints = 0;
				rb.AddForce(vel * 10, ForceMode.Impulse);
			}
		}
	}

	void FixedUpdate()
	{
		if (held)
		{
			vel = held.transform.position - lastPos;
			lastPos = held.transform.position;
		}
	}
}
