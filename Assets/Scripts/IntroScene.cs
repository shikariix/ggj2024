using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntroScene : MonoBehaviour
{

    public string[] fadingTexts;
	public float textLength = 6;
	public CanvasGroup fadeGroup;
	public TextMeshProUGUI textField;

	private void Awake() {
		StartCoroutine(FadeAll());
	}

	private IEnumerator FadeAll() {
		fadeGroup.alpha = 0;
		yield return new WaitForSeconds(1);
		for (int i = 0; i < fadingTexts.Length; i++) {
			textField.text = fadingTexts[i];

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

}
