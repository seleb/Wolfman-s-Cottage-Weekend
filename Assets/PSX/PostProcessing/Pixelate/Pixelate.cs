using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(PixelateRenderer), PostProcessEvent.AfterStack, "Custom/Pixelate")]
public sealed class Pixelate : PostProcessEffectSettings
{
	[Tooltip("Size of Pixelate (in screen pixels)")]
	public Vector2Parameter scale = new Vector2Parameter { value = new Vector2(4, 4) };
}

public sealed class PixelateRenderer : PostProcessEffectRenderer<Pixelate>
{
	public override void Render(PostProcessRenderContext context)
	{
		var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/Pixelate"));
		sheet.properties.SetVector("_Scale", settings.scale);
		context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
	}
}
