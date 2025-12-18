using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TOZ;

namespace TOZ.ImageFX {

	[ExecuteInEditMode]
	public sealed class PP_BlackAndWhite : PostProcessBase {
		//Variables
		[Range(0.0f, 1.0f)]
		public float Threshold = 0.5f;

		//Mono Methods
		void Awake() {
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/BlackAndWhite");
		}

		void OnRenderImage(RenderTexture src, RenderTexture dest) {
			ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		void ApplyVariables() {
			this.mat.SetFloat("_Threshold", Threshold);
		}
	}

}