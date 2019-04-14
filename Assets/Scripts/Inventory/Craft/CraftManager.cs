using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class CraftManager : MonoBehaviour
{


    public FoodItem GetCraft(List<FoodItem> items)
    {
        bool isMeat = false;
        bool isFish = false;
        bool isVegetable = false;

        if (items.Count(i => i.cooked) > 0)
            return null;

        FoodItem returnItem = new FoodItem(Item.Type.FOOD, "Food", true, items.Count*2);
        returnItem.cooked = true;
        return returnItem;
    }


}
