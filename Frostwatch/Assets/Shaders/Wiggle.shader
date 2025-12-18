Shader "Hidden/TOZ/ImageFX/Wiggle" {
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

			uniform float _Speed;
			uniform float _Amplitude;

			fixed4 frag(v2f i) : SV_Target {
				i.uv.x += sin(_Time.y + i.uv.x * _Speed) * _Amplitude;
				i.uv.y += cos(_Time.y + i.uv.y * _Speed) * _Amplitude;
				return tex2D(_MainTex, i.uv);
			}
			ENDCG
		}
	}

	Fallback off
}