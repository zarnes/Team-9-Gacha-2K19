using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public float bonusAttack = 0f;
    public float bonusDefense = 0f;
    public float bonusSpeed = 0f;
    public float bonusJump = 0f;

    public enum itemType {a,b,c}

    public List<Item> items = new List<Item>();


    public void AddItem(Item i)
    {
        items.Add(i);
    }

    public void UseItem(Item i)
    {
        switch (i.type)
        {
            case itemType.a:
                bonusAttack = i.value;
                break;

            case itemType.b:
                bonusDefense = i.value;
                break;

            case itemType.c:
                bonusJump = i.value;
                break;

        }
    }

}
