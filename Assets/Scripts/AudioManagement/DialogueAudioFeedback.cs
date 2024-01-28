using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class DialogueAudioFeedback : MonoBehaviour {

	private Button button;

	private void Start() {
		//button = GetComponent<Button>();
		//button.onClick.AddListener(() => this.ButtonPressed());
	}

	private void OnEnable() {
		button = GetComponent<Button>();
		button.onClick.AddListener(() => this.ButtonPressed());
		if (AudioManager._AudioManager != null)
			AudioManager._AudioManager.PlayOneShot(OneShot.DialogueAdvance);
	}

	public void ButtonPressed() {
		if (AudioManager._AudioManager != null)
			AudioManager._AudioManager.PlayOneShot(OneShot.DialogueAdvance);
	}

}
