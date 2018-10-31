using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Examiner : MonoBehaviour
{

	public CanvasGroup canvasGroup;
	public TMPro.TextMeshProUGUI titleText;
	public TMPro.TextMeshProUGUI descriptionText;
	public List<Pickuper> pickupers = new List<Pickuper>();
	// Use this for initialization
	private GameObject held = null;
	private AudioSource examineSound;
	void Start()
	{
		examineSound = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{
		GameObject anyHeld = null;
		for (var i = 0; i < pickupers.Count; ++i)
		{
			var pickuper = pickupers[i];
			if (pickuper.held)
			{
				anyHeld = pickuper.held;
				break;
			}
		}
		if (anyHeld != held)
		{
			held = anyHeld;
			if (held)
			{
				var examinable = anyHeld.GetComponent<Examinable>();
				titleText.text = examinable ? examinable.titleText : "";
				descriptionText.text = examinable ? examinable.descriptionText : "";
			}
		}

		if (held && titleText.text.Length + descriptionText.text.Length > 0 && held.transform.position.y > Camera.main.transform.position.y + 0.25f)
		{
			if(canvasGroup.alpha <= 0.0f){
				examineSound.Play();
			}
			canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 1.0f, 0.1f);
			if (canvasGroup.alpha > 0.9f)
			{
				canvasGroup.alpha = 1.0f;
			}
		}
		else
		{
			canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 0.0f, 0.1f);
			if (canvasGroup.alpha < 0.1f)
			{
				canvasGroup.alpha = 0.0f;
			}
		}
	}
}
