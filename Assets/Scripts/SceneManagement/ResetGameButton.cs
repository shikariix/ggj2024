using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ResetGameButton : MonoBehaviour
{

	private Button button;
	
	private void Awake() {
		button = GetComponent<Button>();
		button.onClick.AddListener(() => this.ButtonPressed());
	}

	public void ButtonPressed() {
		DressupOutfit._DressupOutfit.Reset();
		Inventory._Inventory.ResetInventory();
	}
}
