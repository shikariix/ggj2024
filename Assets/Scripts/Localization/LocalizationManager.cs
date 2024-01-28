using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{

    private static LocalizationManager localizationManager;

	public Language currentLanguage = Language.Dutch;

	public static LocalizationManager _LocalizationManager { get => localizationManager; set => localizationManager = value; }

	private void Awake() {
		if (localizationManager == null) {
			localizationManager = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else {
			Destroy(this);
		}
	}

	public void SetLanguage(Language newLanguage) {
		currentLanguage = newLanguage;
		foreach (LocalizeStaticText text in FindObjectsOfType<LocalizeStaticText>()) {
			text.SetLanguage(currentLanguage);
		}
	}

}

public enum Language {
	Dutch = 0,
	English = 1
}