using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TOZ;

namespace TOZ.ImageFX {

	[ExecuteInEditMode]
	public sealed class PP_NightVisionV2 : PostProcessBase {
		//Variables
		public Texture2D NoiseTex;
		public Color VisionColor = Color.green;
		public Color FadeColor = Color.black;
		public float NoiseAmount = 1.0f;
		[Range(0.0f, 1.0f)]
		public float Radius = 0.5f;
		[Range(0.0f, 1.0f)]
		public float Fade = 0.2f;
		[Range(0.0f, 1.0f)]
		public float Intensity = 0.5f;
		[Range(0.0f, 2.2f)]
		public float Gamma = 2.2f;

		//Mono Methods
		void Awake() {
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/NightVisionV2");
		}

		void OnRenderImage(RenderTexture src, RenderTexture dest) {
			ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		void ApplyVariables() {
			if(NoiseTex != null)
				this.mat.SetTexture("_NoiseTex", NoiseTex);
			this.mat.SetVector("_VisionColor", VisionColor);
			this.mat.SetVector("_FadeColor", FadeColor);
			this.mat.SetFloat("_NoiseAmount", NoiseAmount);
			this.mat.SetFloat("_Radius", Radius);
			this.mat.SetFloat("_Fade", Fade);
			this.mat.SetFloat("_Intensity", Intensity);
			this.mat.SetFloat("_Gamma", Gamma);
		}
	}

}