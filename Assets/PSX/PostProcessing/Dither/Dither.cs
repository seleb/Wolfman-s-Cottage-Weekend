using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(DitherRenderer), PostProcessEvent.AfterStack, "Custom/Dither")]
public sealed class Dither : PostProcessEffectSettings
{
	[Range(0f, 1f), Tooltip("Dither effect intensity")]
	public FloatParameter blend = new FloatParameter { value = 1f };
	[Range(0f, 512f), Tooltip("Size of dither texture (in screen pixels)")]
	public Vector2Parameter scale = new Vector2Parameter { value = new Vector2(16, 16) };
	[Range(0f, 256f), Tooltip("Number of tones to use")]
	public IntParameter posterize = new IntParameter { value = 32 };
	[Tooltip("Dither mask")]
	public TextureParameter ditherTex = new TextureParameter();
}

public sealed class DitherRenderer : PostProcessEffectRenderer<Dither>
{
	public override void Render(PostProcessRenderContext context)
	{
		var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/Dither"));
		sheet.properties.SetFloat("_Blend", settings.blend);
		sheet.properties.SetVector("_Scale", settings.scale);
		sheet.properties.SetFloat("_Posterize", settings.posterize);
		sheet.properties.SetTexture("_DitherTex", settings.ditherTex);
		context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
	}
}
