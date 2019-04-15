using System.Collections.Generic;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;

using System.Linq;


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
    public Text textFoodGain;
    public Button buttonCraft;

    private CraftManager cm;
    private FoodItem resultItem;
    private List<FoodItem> items;


    private IEnumerator bakeFoodCoroutine;

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

        cm = new CraftManager();
        // todo error
       // buttonCraft.onClick.AddListener(Craft);
    }

    private void Update()
    {
        
        RefreshResult();
    }

    public void RefreshResult()
    {
        items = new List<FoodItem>();

        foreach (Slot slot in slots)
        {
            if (slot.GetItem() is FoodItem)
            {
                FoodItem foodItem = (FoodItem)slot.GetItem();
                items.Add(foodItem);
            }
        }

        resultItem = cm.GetCraft(items);

        textFoodGain.text = "Food : " + resultItem.value;
    } 

    public void Craft()
    {
        bakeFoodCoroutine = BakeFood();
        StartCoroutine(bakeFoodCoroutine);
    }

    IEnumerator BakeFood()
    {
        /*
        if (woodsSlot.IsEmpty())
            return;
            */

        buttonCraft.enabled = false;


        while (items.Count(c => c.cooked)<2 && !woodsSlot.IsEmpty())
        {
            foreach (Slot slot in slots)
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
                        foodItem.cooked = true;
                        slot.SetItem(null);

                    }
                }

            }

            yield return null;
        }

        buttonCraft.enabled = true;

    }

}
