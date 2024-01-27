using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChickenQuest : MonoBehaviour
{
    public QuestObject ActiveQuest;

    //Dialoog bij klikken verkeerde kip
    public string wrongChicken = "(Dat is denk ik niet de kip die ik zoek...)";
    public string englishWrongChicken = "(Then this is I think not the chicken i seek...)";

    //UI elements
    public GameObject dialoguePanel;
    private Button dialogButton;
    public TextMeshProUGUI textMeshPro;

    public GameObject accessoryPanel;
    public Image accessoryImage;

    int currentText = -1;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);
            }
        }
    }
}
