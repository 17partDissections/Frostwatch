#ifndef TOZ_IMAGEFX_INCLUDED
#define TOZ_IMAGEFX_INCLUDED

#include "UnityCG.cginc"

uniform sampler2D _MainTex;
#if UNITY_SINGLE_PASS_STEREO
uniform half4 _MainTex_ST;
#endif
uniform float4 _MainTex_TexelSize;

struct a2v {
	float4 vertex : POSITION;
	half2 texcoord : TEXCOORD0;
	UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct v2f {
	float4 pos : SV_POSITION;
	half2 uv : TEXCOORD0;
	UNITY_VERTEX_INPUT_INSTANCE_ID
	UNITY_VERTEX_OUTPUT_STEREO
};

v2f vert(a2v v) {
	v2f o;
	UNITY_INITIALIZE_OUTPUT(v2f, o);
	UNITY_SETUP_INSTANCE_ID(v);
	UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
	o.pos = UnityObjectToClipPos(v.vertex);
	o.uv = v.texcoord.xy;
	/*
	#ifdef UNITY_HALF_TEXEL_OFFSET
	o.uv.y += _MainTex_TexelSize.y;
	#endif
	#if UNITY_UV_STARTS_AT_TOP
	if(_MainTex_TexelSize.y < 0)
	o.uv.y = 1.0 - o.uv.y;
	#endif
	*/
	#if UNITY_SINGLE_PASS_STEREO
	o.uv = UnityStereoScreenSpaceUVAdjust(o.uv, _MainTex_ST);
	#endif
	return o;
}
#endif