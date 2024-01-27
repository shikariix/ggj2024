using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public class DressupAccessoryButton : MonoBehaviour
{

	public TextMeshProUGUI nameText;
	
	private AccessoryObject accessory;
	private DressupUIManager manager;

	private Button button;
	private Animator animator;

	public AccessoryObject _Accessory { get => accessory; set => accessory = value; }
	public DressupUIManager _Manager { get => manager; set => manager = value; }

	private void Start() {
		button = GetComponent<Button>();
		button.onClick.AddListener(() => this.ButtonPressed());

		animator = GetComponent<Animator>();

		if (accessory != null) {
			nameText.text = accessory.accessoryName;
			if (LocalizationManager._LocalizationManager != null) {
				if (LocalizationManager._LocalizationManager.currentLanguage == Language.Dutch) {
					nameText.text = accessory.accessoryName;
				}
				else {
					nameText.text = accessory.accessoryNameEn;
				}
			}
		}
	}

	public void ButtonPressed() {
		manager.AccessoryButtonPressed(accessory);
	}

	public void SetButtonState(bool accessoryActive) {
		animator.SetBool("Active", accessoryActive);
	}

}
