Shader "Hidden/Custom/Dither"
{
	HLSLINCLUDE

		#include "PostProcessing/Shaders/StdLib.hlsl"

		TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
		TEXTURE2D_SAMPLER2D(_DitherTex, sampler_DitherTex);
		float4 _DitherTex_TexelSize;
		float _Blend;
		float _Posterize;
		float2 _Scale;

		float4 Frag(VaryingsDefault i) : SV_Target
		{
			float2 uv = i.texcoord;
			uv *= _ScreenParams / _Scale;
			uv += 0.5;
			uv = floor(uv);
			uv /= _ScreenParams / _Scale;
			float4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv);
			// posterization
			float3 raw = col.rgb;
			float3 posterized = raw - fmod(raw, 1.0f/_Posterize);

			// dithering
			float2 dit = round(fmod(uv*_ScreenParams, _DitherTex_TexelSize.zw));
			float3 limit = SAMPLE_TEXTURE2D(_DitherTex, sampler_DitherTex, dit/_DitherTex_TexelSize.zw).rgb;
			float3 dither = step(limit, (raw-posterized)*_Posterize)/_Posterize;

			// output
			float3 output = posterized + dither;
			output = lerp(raw, output, _Blend);
			return float4(output.rgb, col.a);
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
