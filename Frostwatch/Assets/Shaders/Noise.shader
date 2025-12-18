Shader "Hidden/TOZ/ImageFX/Noise" {
	Properties {
		_MainTex("Base (RGB)", 2D) = "white" {}
	}

	SubShader {
		Pass {
			Name "BASE"
			Tags { "LightMode" = "Always" }

			ZTest Always Cull Off ZWrite Off Fog { Mode off }

			CGPROGRAM
			#include "Includes/TOZ_ImageFX.cginc"
			#pragma vertex vert
			#pragma fragment frag

			uniform float _Scale;

			fixed4 frag(v2f i) : SV_Target {
				float2 t = i.uv;
				float n = t.x * t.y * 123456 * _Time.y;
				n = fmod(n, 13) * fmod(n, 123);
				float dx = fmod(n, 0.01);
				float dy = fmod(n, 0.012);
				fixed4 col1 = tex2D(_MainTex, t + (float2(dx, dy) * _Scale));
				fixed4 col2 = tex2D(_MainTex, t - (float2(dx, dy) * _Scale));
				return lerp(col1, col2, 0.5);
			}
			ENDCG
		}
	}

	Fallback off
}