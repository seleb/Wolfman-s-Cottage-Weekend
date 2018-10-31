Shader "Hidden/Custom/Pixelate"
{
	HLSLINCLUDE

		#include "PostProcessing/Shaders/StdLib.hlsl"

		TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
		float2 _Scale;
			
		float4 Frag(VaryingsDefault i) : SV_Target
		{
			float2 uv = i.texcoord;
			uv *= _ScreenParams.xy/_Scale;
			uv += 0.5;
			uv = floor(uv);
			uv /= _ScreenParams.xy/_Scale;
			float4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv);
			return col;
		}

	ENDHLSL

	SubShader
	{
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			HLSLPROGRAM

				#pragma vertex VertDefault
				#pragma fragment Frag

			ENDHLSL
		}
	}
}
