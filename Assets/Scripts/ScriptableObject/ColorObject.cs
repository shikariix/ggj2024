using UnityEngine;

[CreateAssetMenu(fileName = "ColorObject_Empty", menuName = "ScriptableObjects/ColorObject", order = 4)]
public class ColorObject : ScriptableObject {
    [Header("Info")]
    public string colorName = "unnamedObject";
    [TextArea(3, 5)]
    public string colorDescription;

    [Header("Settings")]
    public Texture2D colorMap;
    public Color colorA = Color.white;
    public Color colorB = Color.white;
    public Color colorC = Color.white;
}

