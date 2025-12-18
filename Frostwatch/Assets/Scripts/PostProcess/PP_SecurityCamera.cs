using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TOZ;

namespace TOZ.ImageFX {

	[ExecuteInEditMode]
	public sealed class PP_SecurityCamera : PostProcessBase {
		//Variables
		public float Speed = 2.0f;
		[Range(0.0f, 1.0f)]
		public float Thickness = 0.25f;
		[Range(0.0f, 1.0f)]
		public float Luminance = 0.25f;
		[Range(0.0f, 1.0f)]
		public float Darkness = 0.75f;

		//Mono Methods
		void Awake() {
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/SecurityCamera");
		}

		void OnRenderImage(RenderTexture src, RenderTexture dest) {
			ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		void ApplyVariables() {
			this.mat.SetFloat("_Speed", Speed);
			this.mat.SetFloat("_Thickness", Thickness);
			this.mat.SetFloat("_Luminance", Luminance);
			this.mat.SetFloat("_Darkness", Darkness);
		}
	}

}