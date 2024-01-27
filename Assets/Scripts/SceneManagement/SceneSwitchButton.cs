using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SceneSwitchButton : MonoBehaviour {

	public ChickenScene targetScene;

	private Button button;
	//private Animator animator;

	private void Awake() {
		button = GetComponent<Button>();
		button.onClick.AddListener(() => this.ButtonPressed());

		//animator = GetComponent<Animator>();
	}

	public void ButtonPressed() {
		SceneSwitcher._SceneSwitcher.LoadScene(targetScene);
	}

}
