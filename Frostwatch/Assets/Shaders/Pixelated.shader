Shader "Hidden/TOZ/ImageFX/Pixelated" {
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

			uniform float _PixWidth, _PixHeight;

			fixed4 frag(v2f i) : SV_Target {
				float dx = _PixWidth * (_MainTex_TexelSize.x);
				float dy = _PixHeight * (_MainTex_TexelSize.y);
				float2 uv = float2(dx * floor((i.uv.x / dx)), dy * floor((i.uv.y / dy)));
				fixed4 main = tex2D(_MainTex, uv);
				return main;
			}
			ENDCG
		}
	}

	Fallback off
}