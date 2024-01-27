using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LanguageSwapButton : MonoBehaviour
{

	public Image flagUK;
	public Image flagNL;

    private Button button;

	private void Awake() {
		button = GetComponent<Button>();
		button.onClick.AddListener(() => this.ButtonPressed());

		if (LocalizationManager._LocalizationManager == null) {
			Destroy(this.gameObject);
		}
		else {
			flagUK.enabled = LocalizationManager._LocalizationManager.currentLanguage == Language.Dutch;
			flagNL.enabled = LocalizationManager._LocalizationManager.currentLanguage == Language.English;
		}
	}

	public void ButtonPressed() {
		if (LocalizationManager._LocalizationManager.currentLanguage == Language.Dutch) {
			LocalizationManager._LocalizationManager.SetLanguage(Language.English);
		}
		else {
			LocalizationManager._LocalizationManager.SetLanguage(Language.Dutch);
		}

		flagUK.enabled = LocalizationManager._LocalizationManager.currentLanguage == Language.Dutch;
		flagNL.enabled = LocalizationManager._LocalizationManager.currentLanguage == Language.English;
	}
}
