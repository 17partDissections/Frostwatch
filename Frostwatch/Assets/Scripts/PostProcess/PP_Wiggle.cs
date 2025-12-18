using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TOZ;

namespace TOZ.ImageFX {

	[ExecuteInEditMode]
	public sealed class PP_Wiggle : PostProcessBase {
		//Variables
		public float Speed = 10.0f;
		public float Amplitude = 0.05f;

		//Mono Methods
		void Awake() {
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Wiggle");
		}

		void OnRenderImage(RenderTexture src, RenderTexture dest) {
			ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		void ApplyVariables() {
			this.mat.SetFloat("_Speed", Speed);
			this.mat.SetFloat("_Amplitude", Amplitude);
		}
	}

}