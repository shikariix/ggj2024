using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class DressupTabButton : MonoBehaviour
{

    public int id;

	private DressupUIManager manager;

	private Button button;
	private Animator animator;

	public DressupUIManager _Manager { get => manager; set => manager = value; }

	private void Awake() {
		button = GetComponent<Button>();
		button.onClick.AddListener(() => this.ButtonPressed());

		animator = GetComponent<Animator>();
	}

	public void ButtonPressed() {
		manager.TabButtonPressed(id);
	}

	public void SetButtonState(bool accessoryActive) {
		animator.SetBool("Active", accessoryActive);
	}

}
