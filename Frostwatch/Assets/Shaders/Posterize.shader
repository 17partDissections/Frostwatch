Shader "Hidden/TOZ/ImageFX/Posterize" {
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

			uniform int _Colors;
			uniform float _Gamma;

			fixed4 frag(v2f i) : SV_Target {
				fixed4 main = tex2D(_MainTex, i.uv);
				main.rgb = pow(main.rgb, _Gamma) * _Colors;
				main.rgb = floor(main.rgb) / _Colors;
				main.rgb = pow(main.rgb, 1.0 / _Gamma);
				return main;
			}
			ENDCG
		}
	}

	Fallback off
}