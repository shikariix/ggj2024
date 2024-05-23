using UnityEngine;

[CreateAssetMenu(fileName = "QuestObject_Empty", menuName = "ScriptableObjects/QuestObject", order = 5)]
public class QuestObject : ScriptableObject
{
    [Header("Info")]
    public string questName = "unnamedQuest";
    public string questDescription = "Op zoek naar: ";

    [Header("Settings")]
    public AccessoryObject accessory;

    public string[] questDialog;
    public string[] questDialogEn;

    public bool completed = false;
}
