Shader "Hidden/TOZ/ImageFX/BlackAndWhite" {
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

			uniform float _Threshold;

			fixed4 frag(v2f i) : SV_Target {
				fixed4 main = tex2D(_MainTex, i.uv);
				fixed lum = Luminance(main.rgb);
				fixed r = (lum < _Threshold) ? 0 : 1;
				return fixed4(r, r, r, main.a);
			}
			ENDCG
		}
	}

	Fallback off
}