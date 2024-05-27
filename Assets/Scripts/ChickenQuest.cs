using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChickenQuest : MonoBehaviour
{
    public QuestObject ActiveQuest;

    //Dialoog bij klikken verkeerde kip
    public string wrongChicken = "(Dat is denk ik niet de kip die ik zoek...)";
    public string englishWrongChicken = "(Then this is I think not the chicken i seek...)";

    //UI elements
    public DialoguePanel dialoguePanel;
    private Button dialogButton;

    public AccessoryPanel accessoryPanel;

    public QuestText questText;

    int currentText = -1;

    private static ChickenQuest chickenQuest;
    public static ChickenQuest _ChickenQuest { get => chickenQuest; }

    public List<QuestObject> completedQuests = new List<QuestObject>();
   
    public void Awake()
    {
        if (chickenQuest == null)
        {
            chickenQuest = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void OnEnable() {

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        dialoguePanel = GameObject.FindObjectOfType<DialoguePanel>();
        accessoryPanel = GameObject.FindObjectOfType<AccessoryPanel>();
        questText = GameObject.FindObjectOfType<QuestText>();
        if (dialoguePanel) dialoguePanel.gameObject.SetActive(false);
        if (accessoryPanel) accessoryPanel.gameObject.SetActive(false);
        if (questText) questText.gameObject.SetActive(false);

        if (questText && ActiveQuest)
        { 
            questText.text.text = ActiveQuest.questDescription;
            questText.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity);
            if (hit.collider != null)
            {
                if (dialoguePanel.gameObject.activeSelf)
                {
                    return;
                }

                if (hit.collider.gameObject.GetComponent<Interaction>())
                {
                    Interaction interactedChicken = hit.collider.gameObject.GetComponent<Interaction>();
                    if (ActiveQuest)
                    {
                        //als kip goede item heeft, draai dialoog en sluit quest
                        if (ActiveQuest.accessory == interactedChicken.accessory && !interactedChicken.quest)
                        {
                            interactedChicken.enabled = true;
                            //ActiveQuest.completed = true;
                            completedQuests.Add(ActiveQuest);
                            ActiveQuest = null;
                            questText.gameObject.SetActive(false);
                            return;
                        }
                    }
                    else
                    {
                        //als quest is gezet, start quest!
                        if (interactedChicken.quest && !completedQuests.Contains(interactedChicken.quest))
                        {
                            ActiveQuest = interactedChicken.quest;
                            //start dialoog aub
                            interactedChicken.enabled = true;
                        }
                    }
                    
                    //Check of kip item direct mag geven of dat er een quest nodig is
                    if (interactedChicken.giveAccessoryWithoutQuest && !interactedChicken.enabled)
                    {
                        interactedChicken.enabled = true;
                    }
                    else if (!interactedChicken.giveAccessoryWithoutQuest && !interactedChicken.enabled)
                    {
                        //failsafe
                        if (!interactedChicken.dialoguePanel)
                            interactedChicken.setUIElements();
                        interactedChicken.dialoguePanel.gameObject.SetActive(true);
                        interactedChicken.displayText("(Die kip wil nu denk ik niet praten...)");
                    }
                }
            }
        }
    }
}
