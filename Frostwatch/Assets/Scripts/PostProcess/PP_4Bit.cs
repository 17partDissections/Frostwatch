using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TOZ;

namespace TOZ.ImageFX {

	[ExecuteInEditMode]
	public sealed class PP_4Bit : PostProcessBase {
		//Variables
		public int BitDepth = 2;
		[Range(0.0f, 1.0f)]
		public float Contrast = 1f;

		//Mono Methods
		private void Awake() {
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/4Bit");
		}

		private void OnRenderImage(RenderTexture src, RenderTexture dest) {
			ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		private void ApplyVariables() {
			this.mat.SetInt("_BitDepth", BitDepth);
			this.mat.SetFloat("_Contrast", Contrast);
		}
	}

}