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
        if(buttonCraft == null)
            buttonCraft = GameObject.Find("ButtonFurnace").GetComponent<Button>();
        //buttonCraft.onClick.AddListener(Craft);
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


        if (items.Count > 0 && !woodsSlot.IsEmpty())
            buttonCraft.gameObject.SetActive(true);
        else
            buttonCraft.gameObject.SetActive(false);

        resultItem = cm.GetCraft(items);
        
        //textFoodGain.text = "Food : " + resultItem.value;
    } 

    public void Craft()
    {
        bakeFoodCoroutine = BakeFood();
        StartCoroutine(bakeFoodCoroutine);
    }

    public void SetActiveFoodSlots(bool active)
    {
        int ax = 0;

        foreach (Slot s in slots)
        {
            if (s.GetItem() is FoodItem)
            {
                //s.Remove();
                
                slotsUI[ax].IsDisabled = active;
                ax++;
            }
        }
    }

    public void ClearFoodSlots()
    {
        foreach (Slot s in slots)
        {
            if (s.GetItem() is FoodItem)
            {
                s.Remove();
            }
        }
    }

    IEnumerator BakeFood()
    {
            

        SetActiveFoodSlots(false);

        while (items.Count(c => c.cooked)<2 && !woodsSlot.IsEmpty())
        {
            foreach (Slot slot in slots)
            {
                if (slot.IsEmpty())
                    break;
                    
                if (slot.GetItem().type == Item.Type.FOOD)
                {
                    FoodItem foodItem = (FoodItem)slot.GetItem();

                    if (foodItem.cooked)
                        continue;

                    foodItem.bakingAmount += bakingSpeed * Time.deltaTime;

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

        ClearFoodSlots();
        mealSlot.SetItem(resultItem, 1);
        bakeBar.fillAmount = 0f;
        SetActiveFoodSlots(true);
        mealSlot.Add();

    }

}
