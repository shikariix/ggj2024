using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DressupUIManager : MonoBehaviour
{

	[Header("Accessories")]
	public DressupTabButton accessoryTabButton;
    public GameObject accessoryButtonPrefab;
	public GameObject accessoryScrollRect;
	public Transform accessoryButtonContainer;

	[Header("Colors")]
	public DressupTabButton colorTabButton;
	public GameObject colorButtonPrefab;
	public GameObject colorScrollRect;
	public Transform colorButtonContainer;

	private DressupChicken chicken;
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

		accessoryTabButton._Manager = this;
		colorTabButton._Manager = this;

		TabButtonPressed(0);

		chicken = FindObjectOfType<DressupChicken>();
	}

	public void TabButtonPressed(int value) {
		accessoryScrollRect.SetActive(value == 0);
		accessoryTabButton.SetButtonState(value == 0);
		colorScrollRect.SetActive(value != 0);
		colorTabButton.SetButtonState(value != 0);
	}

	public void AccessoryButtonPressed(AccessoryObject accessory) {
		chicken.SetAccessory(accessory);

		foreach (DressupAccessoryButton button in accessoryButtonList) {
			button.SetButtonState(chicken.AccessoryActive(button._Accessory));
		}
	}

	public void ColorButtonPressed(ColorObject color) {
		chicken.SetColor(color);

		foreach (DressupColorButton button in colorButtonList) {
			button.SetButtonState(chicken.CurrentColor() == button._Color);
		}
	}

}
