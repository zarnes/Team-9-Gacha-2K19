using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Furnace : Inventory
{
    public List<Slot> slots;
    public Slot woodsSlot;
    public float bakingSpeed = 2f;

    private void Start()
    {
        foreach (Slot slot in slots)
        {
            slot.Init(this);
        }
        woodsSlot.Init(this);
    }

    private void Update()
    {
        BakeFood();
    }

    public void AddItemAtSlot(int index, Item item)
    {
        if (!item.consumable)
            return;

        slots[index].item = item;
    }

    void BakeFood()
    {
        if (woodsSlot.count == 0)
            return;

        foreach(Slot slot in slots)
        {
            if (slot.item is FoodItem)
            {
                FoodItem foodItem = (FoodItem)slot.item;
                if (foodItem.cooked)
                    continue;
                foodItem.bakingAmount += bakingSpeed * Time.deltaTime;
                if (foodItem.bakingAmount >= foodItem.bakingDuration)
                {
                    //foodItem.cooked = true;
                    slot.Init(this);
                }
            }
            
        }
    }

}
