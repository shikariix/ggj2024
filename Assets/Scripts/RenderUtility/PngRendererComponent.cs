using UnityEngine;
using System.IO;

/// <summary>
/// Render an amount of .png files of the MainCamera. Screenshots can be opaque or use a transparent background.
/// Transparent backgrounds do not work with ARRenderingManager.
/// This class is for use in editor only.
/// </summary>
public class PngRendererComponent : MonoBehaviour
{

	[Header("Manual Trigger")]
	public bool allowManualTrigger = true;
	public KeyCode triggerKey = KeyCode.Return;

	[Header("Image Output")]
	public bool transparentBackground = false;
	[Range(1, 6)]
	public int sizeMultiplier = 1;

	[Header("Sequences")]
	public int amountOfFrames = 1;
	public int captureFramerate = 30;

	private int currentFrame = -1;
	private string dateTime;

	void Update() {
		if (allowManualTrigger && Input.GetKeyDown(triggerKey)) {
			TriggerScreenShot();
		}

		if (amountOfFrames > currentFrame && currentFrame >= 0) {
			Time.captureFramerate = captureFramerate;
			ScreenShot("Sequence_" + dateTime + "_Frame" + currentFrame + ".png", true);
			currentFrame++;
			if (currentFrame == amountOfFrames) {
				currentFrame = -1;
			}
		}
	}

	public void TriggerScreenShot() {
		if (amountOfFrames < 2) {
			ScreenShot();
		}
		else {
			dateTime = GetDateString();
			Time.captureFramerate = captureFramerate;
			currentFrame = 0;
		}
	}

	public void ScreenShot(string fileName = "", bool isSequence = false) {
		if (fileName.Length < 2)
			fileName = GenerateScreenshotFileName();

		string debugLine = transparentBackground ? "Transparent" : "Opaque";
		if (isSequence)
			debugLine += " screenshot rendered, frame " + (currentFrame + 1) + " of " + amountOfFrames;
		else
			debugLine += " screenshot rendered, at time: " + FormatDateString(System.DateTime.Now.ToString("HH:mm:ss"));

		if (transparentBackground) {
			CaptureTransparentScreenshot(fileName, sizeMultiplier);
			Debug.Log(debugLine);
		}
		else {
			ScreenCapture.CaptureScreenshot(fileName, sizeMultiplier);
			Debug.Log(debugLine);
		}
	}

	//sourced from https://answers.unity.com/questions/12070/capture-rendered-scene-to-png-with-background-tran.html
	private void CaptureTransparentScreenshot(string screengrabfile_path, int sizeMultiplier) {
		Camera cam = Camera.main;
		int width = Screen.width * sizeMultiplier;
		int height = Screen.height * sizeMultiplier;
		Color startingColor = cam.backgroundColor;

		// This is slower, but seems more reliable.
		var bak_cam_targetTexture = cam.targetTexture;
		var bak_cam_clearFlags = cam.clearFlags;
		var bak_RenderTexture_active = RenderTexture.active;

		var tex_white = new Texture2D(width, height, TextureFormat.ARGB32, false);
		var tex_black = new Texture2D(width, height, TextureFormat.ARGB32, false);
		var tex_transparent = new Texture2D(width, height, TextureFormat.ARGB32, false);
		// Must use 24-bit depth buffer to be able to fill background.
		var render_texture = RenderTexture.GetTemporary(width, height, 24, RenderTextureFormat.ARGB32);
		var grab_area = new Rect(0, 0, width, height);

		RenderTexture.active = render_texture;
		cam.targetTexture = render_texture;
		cam.clearFlags = CameraClearFlags.SolidColor;

		cam.backgroundColor = Color.black;
		cam.Render();
		tex_black.ReadPixels(grab_area, 0, 0);
		tex_black.Apply();

		cam.backgroundColor = Color.white;
		cam.Render();
		tex_white.ReadPixels(grab_area, 0, 0);
		tex_white.Apply();

		// Create Alpha from the difference between black and white camera renders
		for (int y = 0; y < tex_transparent.height; ++y) {
			for (int x = 0; x < tex_transparent.width; ++x) {
				float alpha = tex_white.GetPixel(x, y).r - tex_black.GetPixel(x, y).r;
				alpha = 1.0f - alpha;
				Color color;
				if (alpha == 0) {
					color = Color.clear;
				}
				else {
					color = tex_black.GetPixel(x, y) / alpha;
				}
				color.a = alpha;
				tex_transparent.SetPixel(x, y, color);
			}
		}

		// Encode the resulting output texture to a byte array then write to the file
		byte[] pngShot = ImageConversion.EncodeToPNG(tex_transparent);
		File.WriteAllBytes(screengrabfile_path, pngShot);

		cam.clearFlags = bak_cam_clearFlags;
		cam.targetTexture = bak_cam_targetTexture;
		RenderTexture.active = bak_RenderTexture_active;
		RenderTexture.ReleaseTemporary(render_texture);

		Texture2D.Destroy(tex_black);
		Texture2D.Destroy(tex_white);
		Texture2D.Destroy(tex_transparent);

		cam.backgroundColor = startingColor;
	}

	private string GenerateScreenshotFileName() {
		return "Screenshot_" + GetDateString() + ".png";
	}

	public string GetDateString() {
		string dateString = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
		dateString = FormatDateString(dateString);
		return dateString;
	}

	private string FormatDateString(string dateString) {
		string formattedDateString = dateString;
		formattedDateString = formattedDateString.Replace("/", "-");
		formattedDateString = formattedDateString.Replace(" ", "_");
		formattedDateString = formattedDateString.Replace(":", "-");
		return formattedDateString;
	}

}
