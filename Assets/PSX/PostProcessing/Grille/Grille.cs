using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(GrilleRenderer), PostProcessEvent.AfterStack, "Custom/Grille")]
public sealed class Grille : PostProcessEffectSettings
{
	[Range(0f, 1f), Tooltip("grille effect intensity")]
	public Vector2Parameter blend = new Vector2Parameter { value = new Vector2(0.5f, 0.5f) };
	[Tooltip("Size of grille (in screen pixels)")]
	public Vector2Parameter scale = new Vector2Parameter { value = new Vector2(4, 4) };
}

public sealed class GrilleRenderer : PostProcessEffectRenderer<Grille>
{
	public override void Render(PostProcessRenderContext context)
	{
		var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/Grille"));
		sheet.properties.SetVector("_Blend", settings.blend);
		sheet.properties.SetVector("_Scale", settings.scale);
		context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
	}
}
