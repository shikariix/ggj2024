using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
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

    int currentText = -1;

    public void Awake()
    {
        dialoguePanel = GameObject.FindObjectOfType<DialoguePanel>();
        accessoryPanel = GameObject.FindObjectOfType<AccessoryPanel>();
    }

    public void OnEnable()
    {
        dialoguePanel.gameObject.SetActive(false);
        accessoryPanel.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.GetComponent<Interaction>())
                {
                    Interaction interactedChicken = hit.collider.gameObject.GetComponent<Interaction>();
                    if (!ActiveQuest)
                    {
                        //Check of kip item direct mag geven of dat er een quest nodig is
                        if (interactedChicken.giveAccessoryWithoutQuest)
                        {
                            interactedChicken.updateText();
                        }
                        //anders: dialoog? deze heb ik nog niet helemaal uitgedacht

                        //als quest is gezet, start quest!
                        if (interactedChicken.quest && !interactedChicken.quest.completed)
                        {
                            ActiveQuest = interactedChicken.quest;
                            //start dialoog aub
                        }
                    }
                    else if (ActiveQuest)
                    {
                        //als kip goede item heeft, draai dialoog en sluit quest
                        if (ActiveQuest.accessory == interactedChicken.accessory)
                        {
                            interactedChicken.updateText();
                            ActiveQuest.completed = true;
                            ActiveQuest = null;
                        }
                    }
                }
            }
        }
    }
}