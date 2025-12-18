Shader "Hidden/TOZ/ImageFX/Amnesia" {
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

			uniform float _Visibility, _Speed;

			fixed4 frag(v2f i) : SV_Target {
				fixed4 main = tex2D(_MainTex, i.uv);
				fixed lum = sin(i.uv.y / _ScreenParams.y + (_Time.y * _Speed)) * 0.5 + 0.5;
				main.rgb *= _Visibility * lum;
				return main;
			}
			ENDCG
		}
	}

	Fallback off
}