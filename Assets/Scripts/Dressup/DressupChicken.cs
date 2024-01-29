using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DressupChicken : MonoBehaviour
{

    public bool isMainCharacter = false;
    
    [Header("Accessory sprite containers")]
    public SpriteRenderer bodyAccessorySprite;
    public SpriteRenderer feetAccessorySprite;
    public SpriteRenderer headAccessorySprite;
    public SpriteRenderer tailAccessorySprite;
    public SpriteRenderer frontWingAccessorySprite;
    public SpriteRenderer backWingAccessorySprite;
    public SpriteRenderer bodyTopAccessorySprite;

    private AccessoryObject bodyAccessory;
    private AccessoryObject feetAccessory;
    private AccessoryObject headAccessory;
    private AccessoryObject tailAccessory;
    private AccessoryObject frontWingAccessory;
    private AccessoryObject backWingAccessory;
    private AccessoryObject bodyTopAccessory;

    [Header("Body sprites")]
    public SpriteRenderer bodySprite;
    public SpriteRenderer frontFootSprite;
    public SpriteRenderer backFootSprite;
    public SpriteRenderer headSprite;
    public SpriteRenderer tailSprite;
    public SpriteRenderer frontWingSprite;
    public SpriteRenderer backWingSprite;

    private ColorObject currentColor;
    private MaterialPropertyBlock propertyBlock;


    private void Awake() {
        bodyAccessorySprite.enabled = false;
        feetAccessorySprite.enabled = false;
        headAccessorySprite.enabled = false;
        tailAccessorySprite.enabled = false;
        frontWingAccessorySprite.enabled = false;
        backWingAccessorySprite.enabled = false;
        bodyTopAccessorySprite.enabled = false;

        propertyBlock = new MaterialPropertyBlock();

        if (GetComponentInChildren<Interaction>()) {
            SetAccessory(GetComponentInChildren<Interaction>().accessory);
        }
    }

	private void Start() {
        if (isMainCharacter && DressupOutfit._DressupOutfit != null) {
            DressupOutfit outfit = DressupOutfit._DressupOutfit;
            SetAccessory(outfit.GetCurrentAccessory(AccessoryType.Body));
            SetAccessory(outfit.GetCurrentAccessory(AccessoryType.Tail));
            SetAccessory(outfit.GetCurrentAccessory(AccessoryType.Feet));
            SetAccessory(outfit.GetCurrentAccessory(AccessoryType.BackWing));
            SetAccessory(outfit.GetCurrentAccessory(AccessoryType.FrontWing));
            SetAccessory(outfit.GetCurrentAccessory(AccessoryType.Head));
            SetAccessory(outfit.GetCurrentAccessory(AccessoryType.BodyTop));

            SetColor(outfit.CurrentColor());
		}
        else if (!isMainCharacter) {
            SetColor(Inventory._Inventory.GetAllInventoryColors()[Random.Range(0, Inventory._Inventory.GetAllInventoryColors().Count)]);
        }
    }

	public void SetAccessory(AccessoryObject accessory, bool canRemove = true) {
        if (accessory != null) {
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
	}

    public bool AccessoryActive (AccessoryObject accessory) {
        return GetCurrentAccessory(accessory.attachmentPoint) == accessory;
	}

    public void SetColor(ColorObject color) {
        if (color != null) {
            if (color.colorMap != null) {
                currentColor = color;

                bodySprite.GetPropertyBlock(propertyBlock);
                propertyBlock.SetTexture("_ColorMap", color.colorMap);
                bodySprite.SetPropertyBlock(propertyBlock);

                frontFootSprite.GetPropertyBlock(propertyBlock);
                propertyBlock.SetTexture("_ColorMap", color.colorMap);
                frontFootSprite.SetPropertyBlock(propertyBlock);

                backFootSprite.GetPropertyBlock(propertyBlock);
                propertyBlock.SetTexture("_ColorMap", color.colorMap);
                backFootSprite.SetPropertyBlock(propertyBlock);

                headSprite.GetPropertyBlock(propertyBlock);
                propertyBlock.SetTexture("_ColorMap", color.colorMap);
                headSprite.SetPropertyBlock(propertyBlock);

                tailSprite.GetPropertyBlock(propertyBlock);
                propertyBlock.SetTexture("_ColorMap", color.colorMap);
                tailSprite.SetPropertyBlock(propertyBlock);

                frontWingSprite.GetPropertyBlock(propertyBlock);
                propertyBlock.SetTexture("_ColorMap", color.colorMap);
                frontWingSprite.SetPropertyBlock(propertyBlock);

                backWingSprite.GetPropertyBlock(propertyBlock);
                propertyBlock.SetTexture("_ColorMap", color.colorMap);
                backWingSprite.SetPropertyBlock(propertyBlock);
            }
        }
	}

    public ColorObject CurrentColor() {
        return currentColor;
	}



    private AccessoryObject GetCurrentAccessory(AccessoryType type) {
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
            case AccessoryType.BodyTop:
                return bodyTopAccessory;
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
            case AccessoryType.BodyTop:
                bodyTopAccessory = remove ? null : accessory;
                break;
            default:
                break;
		}
	}

    private SpriteRenderer GetRenderer(AccessoryType type) {
        switch (type) {
            case AccessoryType.Body:
                return bodyAccessorySprite;
            case AccessoryType.Feet:
                return feetAccessorySprite;
            case AccessoryType.Head:
                return headAccessorySprite;
            case AccessoryType.Tail:
                return tailAccessorySprite;
            case AccessoryType.FrontWing:
                return frontWingAccessorySprite;
            case AccessoryType.BackWing:
                return backWingAccessorySprite;
            case AccessoryType.BodyTop:
                return bodyTopAccessorySprite;
            default:
                return null;
		}
	}

}
