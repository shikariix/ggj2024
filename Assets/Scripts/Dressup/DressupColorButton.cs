using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public class DressupColorButton : MonoBehaviour
{

	public TextMeshProUGUI nameText;
	public Image[] colorSprites;
	
	private ColorObject color;
	private DressupUIManager manager;

	private Button button;
	private Animator animator;

	public ColorObject _Color { get => color; set => color = value; }
	public DressupUIManager _Manager { get => manager; set => manager = value; }

	private void Start() {
		button = GetComponent<Button>();
		button.onClick.AddListener(() => this.ButtonPressed());

		animator = GetComponent<Animator>();

		if (color != null) {
			nameText.text = color.colorName;
			if (colorSprites.Length > 2) {
				colorSprites[0].color = color.colorA;
				colorSprites[1].color = color.colorB;
				colorSprites[2].color = color.colorC;
			}
		}
	}

	public void ButtonPressed() {
		manager.ColorButtonPressed(color);
	}

	public void SetButtonState(bool accessoryActive) {
		animator.SetBool("Active", accessoryActive);
	}

}
