using UnityEngine;

[CreateAssetMenu(fileName = "QuestObject_Empty", menuName = "ScriptableObjects/QuestObject", order = 5)]
public class QuestObject : ScriptableObject
{
    [Header("Info")]
    public string questName = "unnamedQuest";

    [Header("Settings")]
    public AccessoryObject accessory;

    public string[] questDialog;

    public bool completed = false;
}
