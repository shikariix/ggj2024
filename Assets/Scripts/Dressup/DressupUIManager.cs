using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DressupUIManager : MonoBehaviour
{

	[Header("Accessories")]
	public GameObject noAccessoriesButton;
	public DressupTabButton accessoryTabButton;
    public GameObject accessoryButtonPrefab;
	public GameObject accessoryScrollRect;
	public Transform accessoryButtonContainer;

	[Header("Colors")]
	public GameObject noColorsButton;
	public DressupTabButton colorTabButton;
	public GameObject colorButtonPrefab;
	public GameObject colorScrollRect;
	public Transform colorButtonContainer;

	[Header("Proceed")]
	public Button proceedToDateButton;
	public Image proceedToDateLock;

    private List<DressupAccessoryButton> accessoryButtonList = new List<DressupAccessoryButton>();
    private List<DressupColorButton> colorButtonList = new List<DressupColorButton>();

	private void Awake() {
		// Generate accessory buttons
		List<AccessoryObject> accessoryList = Inventory._Inventory.GetAllInventoryAccessories();
		foreach (AccessoryObject accessory in accessoryList) {
			if (accessory.sprite != null) {
				DressupAccessoryButton newButton = Instantiate(accessoryButtonPrefab, accessoryButtonContainer).GetComponent<DressupAccessoryButton>();
				newButton._Accessory = accessory;
				newButton._Manager = this;
				accessoryButtonList.Add(newButton);
			}
		}
		noAccessoriesButton.SetActive(accessoryList.Count == 0);

		// Generate color buttons
		List<ColorObject> colorList = Inventory._Inventory.GetAllInventoryColors();
		foreach (ColorObject color in colorList) {
			if (color.colorMap != null) {
				DressupColorButton newButton = Instantiate(colorButtonPrefab, colorButtonContainer).GetComponent<DressupColorButton>();
				newButton._Color = color;
				newButton._Manager = this;
				colorButtonList.Add(newButton);
			}
		}
		noColorsButton.SetActive(colorList.Count == 0);

		accessoryTabButton._Manager = this;
		colorTabButton._Manager = this;

		TabButtonPressed(0);

		proceedToDateButton.interactable = false;
		proceedToDateLock.enabled = true;
	}

	private void OnEnable() {
		TabButtonPressed(0);

		if (Inventory._Inventory.GetAllInventoryAccessories().Count < 4) {
			proceedToDateButton.interactable = false;
			proceedToDateLock.enabled = true;
		}
		else {
			proceedToDateButton.interactable = true;
			proceedToDateLock.enabled = false;
		}
	}

	public void TabButtonPressed(int value) {
		accessoryScrollRect.SetActive(value == 0);
		accessoryTabButton.SetButtonState(value == 0);
		colorScrollRect.SetActive(value != 0);
		colorTabButton.SetButtonState(value != 0);
	}

	public void AccessoryButtonPressed(AccessoryObject accessory) {
		DressupOutfit._DressupOutfit.SetAccessory(accessory);

		foreach (DressupAccessoryButton button in accessoryButtonList) {
			button.SetButtonState(DressupOutfit._DressupOutfit.AccessoryActive(button._Accessory));
		}
	}

	public void ColorButtonPressed(ColorObject color) {
		DressupOutfit._DressupOutfit.SetColor(color);

		foreach (DressupColorButton button in colorButtonList) {
			button.SetButtonState(DressupOutfit._DressupOutfit.CurrentColor() == button._Color);
		}
	}

}
