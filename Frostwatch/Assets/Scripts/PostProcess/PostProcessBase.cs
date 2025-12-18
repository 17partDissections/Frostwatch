using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TOZ;

namespace TOZ.ImageFX {

	[RequireComponent(typeof(Camera))]
	public abstract class PostProcessBase : MonoBehaviour {
		//Variables
		protected Shader shd;
		protected Material mat;

		//Mono Methods
		private void OnEnable() {
			//Disable if not supported
			if(/*!SystemInfo.supportsImageEffects || */shd == null || !shd.isSupported) {
				this.enabled = false;
				return;
			}

			if(mat == null) {
				mat = new Material(shd);
				mat.hideFlags = HideFlags.HideAndDontSave;
			}
		}

		private void OnDisable() {
			if(mat != null) {
				DestroyImmediate(mat);
			}
		}
	}

}