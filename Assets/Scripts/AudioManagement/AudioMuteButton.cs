using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AudioMuteButton : MonoBehaviour
{

	public GameObject musicOn;
	public GameObject muted;

	private Button button;
	
	private void Start() {
		button = GetComponent<Button>();
		button.onClick.AddListener(() => this.ButtonPressed());

		bool audioOn = AudioManager._AudioManager.Volume > .5f;
		musicOn.SetActive(audioOn);
		muted.SetActive(!audioOn);
	}

	public void ButtonPressed() {
		bool audioOn = AudioManager._AudioManager.Volume < .5f;
		AudioManager._AudioManager.Volume = audioOn ? 1 : 0;
		musicOn.SetActive(audioOn);
		muted.SetActive(!audioOn);
	}

}
