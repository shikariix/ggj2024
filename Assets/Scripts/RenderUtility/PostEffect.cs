using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PostEffect : MonoBehaviour {

	public Material effect;
	public bool useDepthBuffer = true;

	private void OnEnable() {
		if (useDepthBuffer) {
			Camera cam = GetComponent<Camera>();
			cam.depthTextureMode = cam.depthTextureMode | DepthTextureMode.DepthNormals;
		}
	}
			

	void OnRenderImage(RenderTexture source, RenderTexture destination) {
		Graphics.Blit(source, destination, effect);
	}
}