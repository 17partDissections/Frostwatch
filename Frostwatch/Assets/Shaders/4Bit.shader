Shader "Hidden/TOZ/ImageFX/4Bit" {
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

			uniform int _BitDepth;
			uniform float _Contrast;

			fixed4 frag(v2f i) : SV_Target {
				fixed4 main = tex2D(_MainTex, i.uv);
				main.rgb = ((main.rgb - 0.5) * _Contrast) + 0.5;
				fixed bit = (_BitDepth * _BitDepth);
				int3 col = (main.rgb + 1.0 / bit) * bit;
				fixed4 result = fixed4(col.x/bit, col.y/bit, col.z/bit, main.a);
				return result;
			}
			ENDCG
		}
	}

	Fallback off
}