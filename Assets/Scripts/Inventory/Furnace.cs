using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : MonoBehaviour
{
    private Item[] slots = new Item[5];
    public float bakingSpeed = 2f;

    private void Update()
    {
        BakeFood();
    }

    public void AddItemAtSlot(int index, Item item)
    {
        if (!item.consumable)
            return;

        slots[index] = item;
    }

    void BakeFood()
    {
        foreach(Item i in slots)
        {
            i.bakingAmount = Mathf.Clamp(i.bakingAmount,0f,10f);
            i.bakingAmount += bakingSpeed * Time.deltaTime;
        }
    }

}
