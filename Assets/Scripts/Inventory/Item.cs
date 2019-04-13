using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : InventorySystem
{

    public itemType type;
    public float value = 0f;

    public Item(itemType _type, float _value)
    {
        type = _type;
        value = _value;
    }
}
