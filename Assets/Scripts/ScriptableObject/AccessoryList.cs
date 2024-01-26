using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AccessoryList_Empty", menuName = "ScriptableObjects/AccessoryList", order = 1)]
public class AccessoryList : ScriptableObject {
    public AccessoryObject[] accessoryList;
}