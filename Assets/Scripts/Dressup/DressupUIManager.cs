using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DressupUIManager : MonoBehaviour
{

    public GameObject accessoryButtonPrefab;
	public Transform buttonContainer;

	private DressupChicken chicken;
    private List<DressupAccessoryButton> buttonList = new List<DressupAccessoryButton>();

	private void Awake() {
		List<AccessoryObject> accessoryList = Inventory._Inventory.GetAllInventoryAccessories();
		foreach (AccessoryObject accessory in accessoryList) {
			if (accessory.sprite != null) {
				DressupAccessoryButton newButton = Instantiate(accessoryButtonPrefab, buttonContainer).GetComponent<DressupAccessoryButton>();
				newButton._Accessory = accessory;
				newButton._Manager = this;
				buttonList.Add(newButton);
			}
		}

		chicken = FindObjectOfType<DressupChicken>();
	}

	public void AccessoryButtonPressed(AccessoryObject accessory) {
		Debug.Log("Button pressed for " + accessory.accessoryName);
		chicken.SetAccessory(accessory);

		foreach (DressupAccessoryButton button in buttonList) {
			button.SetButtonState(chicken.AccessoryActive(button._Accessory));
		}
	}

}
