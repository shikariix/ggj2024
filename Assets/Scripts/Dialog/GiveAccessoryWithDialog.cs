using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GiveAccessoryWithDialog : MonoBehaviour
{
    public GameObject dialoguePanel;
    private Button dialogButton;
    public TextMeshProUGUI textMeshPro;
    public AccessoryObject accessory;

    public GameObject accessoryPanel;
    public TextMeshProUGUI accessoryTextField;
    public Image accessoryImage;

    int currentText = -1;

    private void OnEnable()
    {
        if (dialogButton == null)
        {
            dialogButton = dialoguePanel.GetComponentInChildren<Button>();
        }
        dialogButton.onClick.RemoveAllListeners();
        dialogButton.onClick.AddListener(updateText);
        accessoryImage.sprite = accessory.sprite;
        accessoryImage.SetNativeSize();
        accessoryTextField.text = accessory.accessoryName;
        if (LocalizationManager._LocalizationManager != null) { if (LocalizationManager._LocalizationManager.currentLanguage == Language.English) { accessoryTextField.SetText(accessory.accessoryNameEn); } }
        updateText();
    }
    void OnMouseDown()
    {
        if (dialoguePanel != null && !dialoguePanel.activeSelf)
        {
            this.enabled = true;
            dialoguePanel.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    public void updateText()
    {
        int dialogLength = accessory.accessoryDialog.Length - 1;
        if (LocalizationManager._LocalizationManager != null) {
            if (LocalizationManager._LocalizationManager.currentLanguage == Language.English) {
                dialogLength = accessory.accessoryDialogEn.Length - 1;
            }
        }
        if (currentText < dialogLength)
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
                textMeshPro.SetText(accessory.accessoryDialog[index]);
            }
			else {
                textMeshPro.SetText(accessory.accessoryDialogEn[index]);
            }
        }
		else {
            textMeshPro.SetText(accessory.accessoryDialog[index]);
        }
    }

    private void closeDialog()
    {
        currentText = -1;
        dialoguePanel.SetActive(false);
        accessoryPanel.SetActive(true);
        Inventory._Inventory.AddItem(accessory);
    }

}
