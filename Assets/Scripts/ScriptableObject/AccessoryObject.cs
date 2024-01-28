using UnityEngine;

[CreateAssetMenu(fileName = "AccessoryObject_Empty", menuName = "ScriptableObjects/AccessoryObject", order = 2)]
public class AccessoryObject : ScriptableObject {
    [Header("Info")]
    public string accessoryName = "unnamedObject";
    public string accessoryNameEn = "unnamedObject";
    [TextArea(3, 5)]
    public string[] accessoryDialog;
    [TextArea(3, 5)]
    public string[] accessoryDialogEn;

    [Header("Settings")]
    public AccessoryType attachmentPoint;
    public Sprite sprite;
}

