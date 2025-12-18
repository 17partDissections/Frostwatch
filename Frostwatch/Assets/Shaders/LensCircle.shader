Shader "Hidden/TOZ/ImageFX/LensCircle" {
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

			uniform float _CenterX, _CenterY;
			uniform float _RadiusX, _RadiusY;

			fixed4 frag(v2f i) : SV_Target {
				fixed4 main = tex2D(_MainTex, i.uv);
				fixed dist = distance(i.uv, float2(_CenterX, _CenterY));
				main.rgb *= smoothstep(_RadiusX, _RadiusY, dist);
				return main;
			}
			ENDCG
		}
	}

	Fallback off
}