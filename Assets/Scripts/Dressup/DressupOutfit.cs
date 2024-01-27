using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DressupOutfit : MonoBehaviour
{

    private static DressupOutfit dressupOutfit;

	private AccessoryObject bodyAccessory;
	private AccessoryObject feetAccessory;
	private AccessoryObject headAccessory;
	private AccessoryObject tailAccessory;
	private AccessoryObject frontWingAccessory;
	private AccessoryObject backWingAccessory;

	private ColorObject currentColor;

	public static DressupOutfit _DressupOutfit { get => dressupOutfit; set => dressupOutfit = value; }

	private void Awake() {
		if (dressupOutfit == null) {
			dressupOutfit = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else {
			Destroy(this);
		}
	}

	public void SetAccessory(AccessoryObject accessory, bool canRemove = true) {
		if (GetCurrentAccessory(accessory.attachmentPoint) == accessory && canRemove) {
			SetCurrentAccessory(accessory, canRemove);
            foreach (DressupChicken chicken in GetChickens()) {
                chicken.SetAccessory(accessory, canRemove);
            }
        }
		else {
			SetCurrentAccessory(accessory);
            foreach (DressupChicken chicken in GetChickens()) {
                chicken.SetAccessory(accessory);
            }
        }
    }

    public bool AccessoryActive(AccessoryObject accessory) {
        return GetCurrentAccessory(accessory.attachmentPoint) == accessory;
    }

    public void SetColor(ColorObject color) {
        if (color.colorMap != null) {

            currentColor = color;
            foreach (DressupChicken chicken in GetChickens()) {
                chicken.SetColor(color);
			}

        }
    }

    public ColorObject CurrentColor() {
        return currentColor;
    }

    public AccessoryObject GetCurrentAccessory(AccessoryType type) {
        switch (type) {
            case AccessoryType.Body:
                return bodyAccessory;
            case AccessoryType.Feet:
                return feetAccessory;
            case AccessoryType.Head:
                return headAccessory;
            case AccessoryType.Tail:
                return tailAccessory;
            case AccessoryType.FrontWing:
                return frontWingAccessory;
            case AccessoryType.BackWing:
                return backWingAccessory;
            default:
                return null;
        }
    }

    private void SetCurrentAccessory(AccessoryObject accessory, bool remove = false) {
        switch (accessory.attachmentPoint) {
            case AccessoryType.Body:
                bodyAccessory = remove ? null : accessory;
                break;
            case AccessoryType.Feet:
                feetAccessory = remove ? null : accessory;
                break;
            case AccessoryType.Head:
                headAccessory = remove ? null : accessory;
                break;
            case AccessoryType.Tail:
                tailAccessory = remove ? null : accessory;
                break;
            case AccessoryType.FrontWing:
                frontWingAccessory = remove ? null : accessory;
                break;
            case AccessoryType.BackWing:
                backWingAccessory = remove ? null : accessory;
                break;
            default:
                break;
        }
    }

    private DressupChicken[] GetChickens() {
        return FindObjectsOfType<DressupChicken>();
	}

	public void Reset() {
        bodyAccessory = null;
        feetAccessory = null;
        headAccessory = null;
        tailAccessory = null;
        frontWingAccessory = null;
        backWingAccessory = null;

        currentColor = null;
    }

}
