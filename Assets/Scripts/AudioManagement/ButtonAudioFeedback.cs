using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class ButtonAudioFeedback : MonoBehaviour, IPointerEnterHandler {

	private Button button;

	private void Start() {
		button = GetComponent<Button>();
		button.onClick.AddListener(() => this.ButtonPressed());
	}

	public void ButtonPressed() {
		if (AudioManager._AudioManager != null)
			AudioManager._AudioManager.PlayOneShot(OneShot.ButtonPress);
	}

	public void OnPointerEnter(PointerEventData eventData) {
		if (AudioManager._AudioManager != null)
			AudioManager._AudioManager.PlayOneShot(OneShot.ButtonHover);
	}

}
