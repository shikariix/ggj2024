using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    public DialoguePanel dialoguePanel;
    public AccessoryPanel accessoryPanel;

    public bool giveAccessoryWithoutQuest = false;
    public AccessoryObject accessory;
    public QuestObject quest;

    private ChickenMovement chickenMovement;

    int currentText = -1;

    private void Start()
    {
        if (!dialoguePanel) dialoguePanel = ChickenQuest._ChickenQuest.dialoguePanel;
        if (!accessoryPanel) accessoryPanel = ChickenQuest._ChickenQuest.accessoryPanel;

        chickenMovement = GameObject.FindObjectOfType<ChickenMovement>();
    }
    private void OnEnable()
    {
        dialoguePanel.dialogueButton.onClick.RemoveAllListeners();
        dialoguePanel.dialogueButton.onClick.AddListener(() => updateText());
        accessoryPanel.accessoryImage.sprite = accessory.sprite;
        accessoryPanel.accessoryImage.SetNativeSize();
        accessoryPanel.accessoryTextField.text = accessory.accessoryName;
        if (LocalizationManager._LocalizationManager != null) { if (LocalizationManager._LocalizationManager.currentLanguage == Language.English) { accessoryPanel.accessoryTextField.SetText(accessory.accessoryNameEn); } }
        updateText();
    }
    public void activateDialog()
    {
        if (dialoguePanel != null && !dialoguePanel.gameObject.activeSelf)
        {
            chickenMovement.enabled = false;
            this.enabled = true;
            dialoguePanel.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    public void updateText()
    {
        if (!dialoguePanel.gameObject.activeSelf)
        {
            dialoguePanel.gameObject.SetActive(true);
        }

        int dialogLength = accessory.accessoryDialog.Length -1;
        if (LocalizationManager._LocalizationManager != null) {
            if (LocalizationManager._LocalizationManager.currentLanguage == Language.English) {
                dialogLength = accessory.accessoryDialogEn.Length -1;
            }
        }

        if (currentText == -1)
        {
            currentText = 0;
            displayText(currentText);
        }
        else if (currentText < dialogLength)
        {
            currentText++;
            displayText(currentText);
        }
        else
        {
            closeDialog();
        }
    }

    public void displayText(int index)
    {
        if (LocalizationManager._LocalizationManager != null) {
            if (LocalizationManager._LocalizationManager.currentLanguage == Language.Dutch) {
                dialoguePanel.dialogueText.SetText(accessory.accessoryDialog[index]);
            }
			else {
                dialoguePanel.dialogueText.SetText(accessory.accessoryDialogEn[index]);
            }
        }
		else {
            dialoguePanel.dialogueText.SetText(accessory.accessoryDialog[index]);
        }
    }

    private void closeDialog()
    {
        currentText = -1;
        dialoguePanel.gameObject.SetActive(false);
        chickenMovement.enabled = true;
        this.enabled = false;
        if (!Inventory._Inventory.InventoryContainsAccessory(accessory))
        {
            Inventory._Inventory.AddItem(accessory);
            accessoryPanel.gameObject.SetActive(true);
        }
    }

}
