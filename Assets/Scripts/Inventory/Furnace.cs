using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Furnace : Inventory
{
    public List<Slot> slots;
    public List<SlotUI> slotsUI;
    public Slot woodsSlot;
    public SlotUI woodsSlotUI;
    public Slot mealSlot;
    public SlotUI mealSlotUI;
    public float bakingSpeed = 2f;

    public Image bakeBar;

    private void Start()
    {
        if (slots.Count != slotsUI.Count)
        {
            Debug.LogError("Inventory size doesn't fit with inventory UI", gameObject);
        }
        for (int i = 0; i < slots.Count; ++i)
        {
            slots[i].SetInventory(this);
            slotsUI[i].Init(slots[i]);
        }
        woodsSlot.SetInventory(this);
        woodsSlotUI.Init(woodsSlot);
        mealSlot.SetInventory(this);
        mealSlotUI.Init(mealSlot);
    }

    private void Update()
    {
        BakeFood();
    }

    void BakeFood()
    {
        if (woodsSlot.IsEmpty())
            return;

        foreach(Slot slot in slots)
        {
            if (slot.GetItem() is FoodItem)
            {
                FoodItem foodItem = (FoodItem)slot.GetItem();
                if (foodItem.cooked)
                    continue;

                foodItem.bakingAmount += bakingSpeed * Time.deltaTime;
                Debug.Log("cook");
                bakeBar.fillAmount = foodItem.bakingAmount / foodItem.bakingDuration;

                if (foodItem.bakingAmount >= foodItem.bakingDuration)
                {
                    //foodItem.cooked = true;
                    slot.SetItem(null);
                }
            }
            
        }
    }

}
