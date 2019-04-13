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
                bonusAttack = 5f;
                break;

            case itemType.b:
                bonusDefense = 5f;
                break;

            case itemType.c:
                bonusJump = 10f;
                break;

        }
    }

}
