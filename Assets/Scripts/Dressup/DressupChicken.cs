using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DressupChicken : MonoBehaviour
{
    
    [Header("Accessory sprite containers")]
    public SpriteRenderer bodySprite;
    public SpriteRenderer feetSprite;
    public SpriteRenderer headSprite;
    public SpriteRenderer tailSprite;
    public SpriteRenderer frontWingSprite;
    public SpriteRenderer backWingSprite;

    private AccessoryObject bodyAccessory;
    private AccessoryObject feetAccessory;
    private AccessoryObject headAccessory;
    private AccessoryObject tailAccessory;
    private AccessoryObject frontWingAccessory;
    private AccessoryObject backWingAccessory;

	private void Awake() {
        bodySprite.enabled = false;
        feetSprite.enabled = false;
        headSprite.enabled = false;
        tailSprite.enabled = false;
        frontWingSprite.enabled = false;
        backWingSprite.enabled = false;
	}

	public void SetAccessory(AccessoryObject accessory, bool canRemove = true) {
        if (GetCurrentAccessory(accessory.attachmentPoint) == accessory && canRemove) {
            SetCurrentAccessory(accessory, canRemove);
            GetRenderer(accessory.attachmentPoint).enabled = false;
        }
		else {
            SetCurrentAccessory(accessory);
            GetRenderer(accessory.attachmentPoint).sprite = accessory.sprite;
            GetRenderer(accessory.attachmentPoint).enabled = true;
        }
	}

    public bool AccessoryActive (AccessoryObject accessory) {
        return GetCurrentAccessory(accessory.attachmentPoint) == accessory;
	}



    private AccessoryObject GetCurrentAccessory(AccessoryType type) {
        switch (type) {
            case AccessoryType.Body:
                return bodyAccessory;
                break;
            case AccessoryType.Feet:
                return feetAccessory;
                break;
            case AccessoryType.Head:
                return headAccessory;
                break;
            case AccessoryType.Tail:
                return tailAccessory;
                break;
            case AccessoryType.FrontWing:
                return frontWingAccessory;
                break;
            case AccessoryType.BackWing:
                return backWingAccessory;
                break;
            default:
                return null;
                break;
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

    private SpriteRenderer GetRenderer(AccessoryType type) {
        switch (type) {
            case AccessoryType.Body:
                return bodySprite;
                break;
            case AccessoryType.Feet:
                return feetSprite;
                break;
            case AccessoryType.Head:
                return headSprite;
                break;
            case AccessoryType.Tail:
                return tailSprite;
                break;
            case AccessoryType.FrontWing:
                return frontWingSprite;
                break;
            case AccessoryType.BackWing:
                return backWingSprite;
                break;
            default:
                return null;
                break;
		}
	}

}
