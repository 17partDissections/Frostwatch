using UnityEngine;

namespace TOZ.ImageFX {

	[ExecuteInEditMode]
	public sealed class PP_Pixelated : PostProcessBase {
		//Variables
		public int PixelWidth = 16;
		public int PixelHeight = 16;

		//Mono Methods
		void Awake() {
			if(PlayerPrefs.GetInt("PixelationValue") == 0)
				this.enabled = false;
			this.shd = Shader.Find("Hidden/TOZ/ImageFX/Pixelated");
		}

		void OnRenderImage(RenderTexture src, RenderTexture dest) {
			ApplyVariables();
			Graphics.Blit(src, dest, this.mat);
		}

		void ApplyVariables() {
			this.mat.SetFloat("_PixWidth", PixelWidth);
			this.mat.SetFloat("_PixHeight", PixelHeight);
		}
	}

}