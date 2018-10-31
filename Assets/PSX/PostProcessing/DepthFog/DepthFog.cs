using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(DepthFogRenderer), PostProcessEvent.BeforeStack, "Custom/DepthFog")]
public sealed class DepthFog : PostProcessEffectSettings
{
	[Range(0f, 1f), Tooltip("Fog effect intensity")]
	public FloatParameter blend = new FloatParameter { value = 1f };
	[Range(0f, 1f), Tooltip("Fog start")]
	public FloatParameter start = new FloatParameter { value = 0.5f };
	[Range(0f, 1f), Tooltip("Fog end")]
	public FloatParameter end = new FloatParameter { value = 1f };
	[Tooltip("1D fog gradient")]
	public TextureParameter depthFogTex = new TextureParameter();
}

public sealed class DepthFogRenderer : PostProcessEffectRenderer<DepthFog>
{
	public override void Render(PostProcessRenderContext context)
	{
		var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/DepthFog"));
		sheet.properties.SetFloat("_Blend", settings.blend);
		sheet.properties.SetFloat("_Start", settings.start);
		sheet.properties.SetFloat("_End", settings.end);
		sheet.properties.SetTexture("_DepthFogTex", settings.depthFogTex);
		context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
	}
}
