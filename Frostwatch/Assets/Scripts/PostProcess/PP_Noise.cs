using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TOZ;

namespace TOZ.ImageFX {

	[ExecuteInEditMode]
	public sealed class PP_Noise : PostProcessBase {
		//Variables
		[Range(0.0f, 2.0f)]
		public float Scale = 0.5f;

		//Mono Methods
		void Awake() {
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Noise");
		}

		void OnRenderImage(RenderTexture src, RenderTexture dest) {
			ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		void ApplyVariables() {
			this.mat.SetFloat("_Scale", Scale);
		}
	}

}