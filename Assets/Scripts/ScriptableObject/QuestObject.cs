using UnityEngine;

[CreateAssetMenu(fileName = "QuestObject_Empty", menuName = "ScriptableObjects/QuestObject", order = 5)]
public class QuestObject : ScriptableObject
{
    [Header("Info")]
    public string questName = "unnamedQuest";

    [Header("Settings")]
    public AccessoryObject accessory;

    public bool instantGet = false;
    /// <summary>
    /// Only relevant if above is false.
    /// </summary>
    public string[] questDialog;

    [HideInInspector]
    public bool completed;
}
