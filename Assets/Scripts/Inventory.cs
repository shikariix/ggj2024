using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public AccessoryList allAccessories;
    public bool debug = false;

    private static Inventory inventory;

    private List<AccessoryObject> accessoryList = new List<AccessoryObject>();

	public static Inventory _Inventory { get => inventory; }

	private void Awake() {
		if (inventory == null) {
            inventory = this;
            DontDestroyOnLoad(this.gameObject);
        }
		else {
            Destroy(this);
		}
	}

	public void AddItem(string newAccessoryName) {
        for (int i = 0; i < allAccessories.accessoryList.Length; i++) {
            if (allAccessories.accessoryList[i].accessoryName == newAccessoryName) {
                AddItem(allAccessories.accessoryList[i]);
                i = allAccessories.accessoryList.Length + 9000; // stop the for-loop
			}
		}
	}
    public void AddItem(AccessoryObject newAccessory) {
        bool alreadyObtained = false;
        for (int i = 0; i < accessoryList.Count; i++) {
            if (accessoryList[i].accessoryName == newAccessory.accessoryName) { // check if the item being added was already in the list
                alreadyObtained = true;
            }
        }

        if (alreadyObtained == false) {
            accessoryList.Add(newAccessory); // add to the list
        }
	}

    public List<AccessoryObject> GetAllInventoryAccessories() {
        if (debug) {
            List<AccessoryObject> all = new List<AccessoryObject>(allAccessories.accessoryList);
            return all;
		}
		else {
            return accessoryList;
        }
	}

    public List<AccessoryObject> GetUnobtainedAccessories() {
        List<AccessoryObject> unobtainedList = new List<AccessoryObject>();
        for (int i = 0; i < allAccessories.accessoryList.Length; i++) {
            if (!accessoryList.Contains(allAccessories.accessoryList[i])) {
                unobtainedList.Add(allAccessories.accessoryList[i]);
            }
        }
        return unobtainedList;
    }

    public bool InventoryContainsAccessory(AccessoryObject accessory) {
        return accessoryList.Contains(accessory);
    }

}
