Shader "Hidden/Custom/DepthFog"
{
	HLSLINCLUDE

		#include "PostProcessing/Shaders/StdLib.hlsl"

		TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
		TEXTURE2D_SAMPLER2D(_DepthFogTex, sampler_DepthFogTex);
		TEXTURE2D_SAMPLER2D(_CameraDepthTexture, sampler_CameraDepthTexture);
		float _Blend;
		float _Start;
		float _End;
		float2 _Scale;

		float map(float value, float fromMin, float fromMax, float toMin, float toMax){
			return ((value - fromMin) / (fromMax - fromMin)) * (toMax - toMin) + toMin;
		}

		float4 Frag(VaryingsDefault i) : SV_Target
		{
			half4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord);

			float depth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, i.texcoord);
			depth = Linear01Depth(depth);
			depth = map(depth, _Start, _End, 0.0f, 1.0f);
			depth = clamp(depth, 0.0f, 1.0f);
			half3 depthCol = SAMPLE_TEXTURE2D(_DepthFogTex, sampler_DepthFogTex, float2(depth, 2.0f*distance(i.texcoord.x, 0.5f))).rgb;
			return float4(lerp(col.rgb, depthCol, depth*_Blend), col.a);
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
