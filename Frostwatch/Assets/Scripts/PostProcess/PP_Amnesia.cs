using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TOZ;

namespace TOZ.ImageFX {

	[ExecuteInEditMode]
	public sealed class PP_Amnesia : PostProcessBase {
		//Variables
		[Range(0f, 1f)]
		public float Visibility = 1.0f;
		public float Speed = 3.0f;

		//Mono Methods
		private void Awake() {
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Amnesia");
		}

		private void OnRenderImage(RenderTexture src, RenderTexture dest) {
			ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		private void ApplyVariables() {
			this.mat.SetFloat("_Visibility", Visibility);
			this.mat.SetFloat("_Speed", Speed);
		}
	}

}