using System;
using UnityEngine;

namespace TOZ.ImageFX {

	[ExecuteInEditMode]
	public sealed class PP_LensCircle : PostProcessBase {
		//Variables
		[Range(0f, 1f)]
		public float CenterX = 0.5f;
		[Range(0f, 1f)]
		public float CenterY = 0.5f;
		[Range(0f, 1f)]
		public float RadiusX = 1.0f;
		[Range(0f, 1f)]
		public float RadiusY = 0.0f;

		//Mono Methods
		void Awake() {
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/LensCircle");
		}

		void OnRenderImage(RenderTexture src, RenderTexture dest) {
			ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		void ApplyVariables() {
			this.mat.SetFloat("_CenterX", CenterX);
			this.mat.SetFloat("_CenterY", CenterY);
			this.mat.SetFloat("_RadiusX", RadiusX);
			this.mat.SetFloat("_RadiusY", RadiusY);
		}
	}

}