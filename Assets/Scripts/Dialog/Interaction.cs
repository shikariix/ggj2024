using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    public DialoguePanel dialoguePanel;
    private Button dialogButton;

    public AccessoryPanel accessoryPanel;

    public bool giveAccessoryWithoutQuest = false;
    public AccessoryObject accessory;
    public QuestObject quest;

    int currentText = -1;

    private void Awake()
    {
        dialoguePanel = ChickenQuest._ChickenQuest.dialoguePanel;
        accessoryPanel = ChickenQuest._ChickenQuest.accessoryPanel;
    }
    private void OnEnable()
    {
        if (dialogButton == null)
        {
            dialogButton = dialoguePanel.GetComponentInChildren<Button>();
        }
        dialogButton.onClick.RemoveAllListeners();
        dialogButton.onClick.AddListener(updateText);
        accessoryPanel.accessoryImage.sprite = accessory.sprite;
        accessoryPanel.accessoryImage.SetNativeSize();
        accessoryPanel.accessoryTextField.text = accessory.accessoryName;
        if (LocalizationManager._LocalizationManager != null) { if (LocalizationManager._LocalizationManager.currentLanguage == Language.English) { accessoryPanel.accessoryTextField.SetText(accessory.accessoryNameEn); } }
        updateText();
    }
    void OnMouseDown()
    {
        if (dialoguePanel != null && !dialoguePanel.gameObject.activeSelf)
        {
            this.enabled = true;
            dialoguePanel.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    public void updateText()
    {
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
        accessoryPanel.gameObject.SetActive(true);
        Inventory._Inventory.AddItem(accessory);
    }

}
