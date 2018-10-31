Shader "Hidden/Custom/Grille"
{
	HLSLINCLUDE

		#include "PostProcessing/Shaders/StdLib.hlsl"

		TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
		float2 _Blend;
		float2 _Scale;
			
			// g *= 2.0;
			// g -= 1.0;
			// g = abs(g);
			// g = 1.0 - g*_Scale*clamp(pixelScale-2.0, 0,1);
			// return g;
			// return 1.0-pow(1.0-g.y*g.x,2.0);
		// }

		float4 Frag(VaryingsDefault i) : SV_Target
		{
			float4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord);
			float2 g = frac(i.texcoord*_ScreenParams.xy/_Scale);
			g *= 2.0;
			g -= 1.0;
			g = abs(g);
			g = 1.0 - g * _Blend;
			// g /= 2.0;
			// output
			float3 output = col.rgb * g.x * g.y;
			// output = lerp(col, output, _Blend);
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
