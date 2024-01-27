using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorList_Empty", menuName = "ScriptableObjects/ColorList", order = 3)]
public class ColorList : ScriptableObject {
    public ColorObject[] colorList;
}