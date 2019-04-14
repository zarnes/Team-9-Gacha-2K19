using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : InventorySystem
{

    public itemType type;
    public float value = 0f;
    public bool consumable = false;
    public float bakingAmount = 0f;

    public Item(itemType _type, float _value, bool _consumable)
    {
        type = _type;
        value = _value;
        consumable = _consumable;
    }

    public Item()
    {

    }
}
