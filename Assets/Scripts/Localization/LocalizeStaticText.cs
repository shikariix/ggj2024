using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocalizeStaticText : MonoBehaviour
{

	public TextMeshProUGUI textField;

	[TextArea(3, 5)]
	public string dutchText;
	[TextArea(3, 5)]
	public string englishText;
	
	private void Awake() {
		if (textField == null) {
			textField = GetComponent<TextMeshProUGUI>();
		}
		if (textField == null) {
			Destroy(this);
		}

		if (LocalizationManager._LocalizationManager != null) {
			SetLanguage(LocalizationManager._LocalizationManager.currentLanguage);
		}
		else {
			SetLanguage(Language.Dutch);
		}
	}

	public void SetLanguage(Language newLanguage) {
		switch (newLanguage) {
			case Language.Dutch:
				textField.text = dutchText;
				break;
			case Language.English:
				textField.text = englishText;
				break;
			default:
				break;
		}
	}

}
