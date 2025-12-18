Shader "Hidden/TOZ/ImageFX/SecurityCamera" {
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
			uniform float _Thickness;
			uniform float _Luminance;
			uniform float _Darkness;

			fixed4 frag(v2f i) : SV_Target {
				fixed4 main = tex2D(_MainTex, i.uv);
				float cycle = sin((i.uv.y / _MainTex_TexelSize.y - _Time.y * _Speed) *_Thickness);
				main.rgb *= _Darkness + _Luminance * cycle;
				return main;
			}
			ENDCG
		}
	}

	Fallback off
}