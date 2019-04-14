using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{


    public enum itemType {wood,food,c}

    public List<Item> items = new List<Item>();


    public void AddItem(Item i)
    {
        items.Add(i);
    }

    public void RemoveItem(Item i)
    {
        items.Remove(i);
    }

    public void DropItem(Item i)
    {
        items.Remove(i);

    }

    public void UseItem(Item i)
    {
        switch (i.type)
        {
            case itemType.wood:

                break;

            case itemType.food:

                break;

            case itemType.c:

                break;

        }
    }

}
