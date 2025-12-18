using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TOZ;

namespace TOZ.ImageFX {

	[ExecuteInEditMode]
	public sealed class PP_Posterize : PostProcessBase {
		//Variables
		public int Colors = 4;
		[Range(0.0f, 2.2f)]
		public float Gamma = 1f;

		//Mono Methods
		void Awake() {
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Posterize");
		}

		void OnRenderImage(RenderTexture src, RenderTexture dest) {
			ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		void ApplyVariables() {
			this.mat.SetInt("_Colors", Colors);
			this.mat.SetFloat("_Gamma", Gamma);
		}
	}

}