using UnityEngine;

[CreateAssetMenu(fileName = "AccessoryObject_Empty", menuName = "ScriptableObjects/AccessoryObject", order = 2)]
public class AccessoryObject : ScriptableObject {
    [Header("Info")]
    public string accessoryName = "unnamedObject";
    [TextArea(3, 5)]
    public string accessoryDescription;

    [Header("Settings")]
    public AccessoryType attachmentPoint;
    public Sprite sprite;

    [Header("Unused Settings That I Would Love To Add")]
    public Color color = Color.white;
}

