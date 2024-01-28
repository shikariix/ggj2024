using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntroScene : MonoBehaviour
{

    public string[] fadingTexts;
    public string[] englishFadingTexts;
	public float textLength = 6;
	public CanvasGroup fadeGroup;
	public TextMeshProUGUI textField;

	private void Start() {
		if (LocalizationManager._LocalizationManager != null) {
			if (LocalizationManager._LocalizationManager.currentLanguage == Language.Dutch) {
				StartCoroutine(FadeAll(fadingTexts));
			}
			else {
				StartCoroutine(FadeAll(englishFadingTexts));
			}
		}
		else {
			StartCoroutine(FadeAll(fadingTexts));
		}
	}

	private IEnumerator FadeAll(string[] texts) {
		fadeGroup.alpha = 0;
		if (AudioManager._AudioManager != null) {
			if (AudioManager._AudioManager.Volume > .1f) {
				StartCoroutine(AudioFadeIn(1 + texts.Length * textLength));
			}
		}
		yield return new WaitForSeconds(1);
		for (int i = 0; i < texts.Length; i++) {
			textField.text = texts[i];

			for (float f = 0; f < .5f; f += Time.deltaTime) {
				fadeGroup.alpha = f * 2;
				yield return null;
			}
			fadeGroup.alpha = 1;

			yield return new WaitForSeconds(textLength - 1);

			for (float f = 0; f < .5f; f += Time.deltaTime) {
				fadeGroup.alpha = 1 - f * 2;
				yield return null;
			}
			fadeGroup.alpha = 0;
			yield return null;
		}
		SceneSwitcher._SceneSwitcher.LoadScene(ChickenScene.MainMenu);
	}

	private IEnumerator AudioFadeIn(float length) {
		AudioManager._AudioManager.ChickensReverbVolume = 0;
		yield return new WaitForSeconds(length / 2);
		for (float f = 0; f < length * .5f; f += Time.deltaTime) {
			AudioManager._AudioManager.ChickensReverbVolume = f / (length * .5f);
			yield return null;
		}
	}

}
