using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
    Material,
    Consumable,
    Usable,
    Default
}

public class ItemObject : ScriptableObject
{
    public GameObject prefab;
    public ItemType type;
    [TextArea(20,15)]
    public string description;
}
