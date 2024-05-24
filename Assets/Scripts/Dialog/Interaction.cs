using System;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class Interaction : MonoBehaviour
{
    public DialoguePanel dialoguePanel;
    public AccessoryPanel accessoryPanel;

    public bool giveAccessoryWithoutQuest = false;
    public AccessoryObject accessory;
    public QuestObject quest;

    public QuestText questText;

    private ChickenMovement chickenMovement;

    int currentText = -1;

    private void Awake()
    {
        if (!dialoguePanel) dialoguePanel = ChickenQuest._ChickenQuest.dialoguePanel;
        if (!accessoryPanel) accessoryPanel = ChickenQuest._ChickenQuest.accessoryPanel;
        if (!questText) questText = ChickenQuest._ChickenQuest.questText;
        chickenMovement = GameObject.FindObjectOfType<ChickenMovement>();
    }
    private void OnEnable()
    {
        dialoguePanel.dialogueButton.onClick.RemoveAllListeners();
        dialoguePanel.dialogueButton.onClick.AddListener(() => updateText());
        if (accessory)
        {
            accessoryPanel.accessoryImage.sprite = accessory.sprite;
            accessoryPanel.accessoryImage.SetNativeSize();
            accessoryPanel.accessoryTextField.text = accessory.accessoryName;
            if (LocalizationManager._LocalizationManager != null) { if (LocalizationManager._LocalizationManager.currentLanguage == Language.English) { accessoryPanel.accessoryTextField.SetText(accessory.accessoryNameEn); } }
        }
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

        int dialogLength = 0;

        if (quest)
        {
            dialogLength = quest.questDialog.Length - 1;
            if (LocalizationManager._LocalizationManager != null)
            {
                if (LocalizationManager._LocalizationManager.currentLanguage == Language.English)
                {
                    dialogLength = quest.questDialogEn.Length - 1;
                }
            }
        }
        else if (accessory)
        {
            dialogLength = accessory.accessoryDialog.Length -1;
            if (LocalizationManager._LocalizationManager != null)
            {
                if (LocalizationManager._LocalizationManager.currentLanguage == Language.English)
                {
                    dialogLength = accessory.accessoryDialogEn.Length - 1;
                }
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

    public void displayText(string customText)
    {
        dialoguePanel.dialogueText.SetText(customText);
        //hack
        dialoguePanel.dialogueButton.onClick.RemoveAllListeners();
        dialoguePanel.dialogueButton.onClick.AddListener(() => closeDialog(false));
    }

    public void displayText(int index)
    {
        if (quest)
        {
            if (LocalizationManager._LocalizationManager != null)
            {
                if (LocalizationManager._LocalizationManager.currentLanguage == Language.Dutch)
                {
                    dialoguePanel.dialogueText.SetText(quest.questDialog[index]);
                }
                else
                {
                    dialoguePanel.dialogueText.SetText(quest.questDialogEn[index]);
                }
            }
            else
            {
                dialoguePanel.dialogueText.SetText(quest.questDialog[index]);
            }
        }
        else if (accessory)
        {
            if (LocalizationManager._LocalizationManager != null)
            {
                if (LocalizationManager._LocalizationManager.currentLanguage == Language.Dutch)
                {
                    dialoguePanel.dialogueText.SetText(accessory.accessoryDialog[index]);
                }
                else
                {
                    dialoguePanel.dialogueText.SetText(accessory.accessoryDialogEn[index]);
                }
            }
            else
            {
                dialoguePanel.dialogueText.SetText(accessory.accessoryDialog[index]);
            }
        }
        
    }

    private void closeDialog(bool giveItem = true)
    {
        currentText = -1;
        dialoguePanel.gameObject.SetActive(false);
        chickenMovement.enabled = true;
        this.enabled = false;
        if (quest && !quest.completed && !questText.gameObject.activeInHierarchy)
        {
            questText.gameObject.SetActive(true);
            questText.text.text = quest.questDescription;
        }

        if (accessory && !Inventory._Inventory.InventoryContainsAccessory(accessory) && giveItem && !quest)
        {
            Inventory._Inventory.AddItem(accessory);
            accessoryPanel.gameObject.SetActive(true);
            if (questText && !giveAccessoryWithoutQuest) questText.gameObject.SetActive(false);
        }
    }

}
