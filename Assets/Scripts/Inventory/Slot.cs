using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int id = 0;
    public Inventory inventory;
    public Item item;
    public int count = 1;
    public Image image;

    public void Init(Inventory _inventory)
    {
        inventory = _inventory;
        RefreshUI();
    }

    public void RefreshUI()
    {
        if (item != null)
        {
            image.sprite = item.getIcon();
        }
        else
        {
            image.sprite = null;
        }
    }

    private void OnMouseDown()
    {
        inventory.OnSlotClicked(this);
        /*
        GameObject.FindObjectOfType<InventorySystem>().isDraging = true;
        GameObject.FindObjectOfType<InventorySystem>().startSlot = id;
        */
    }

    private void OnMouseUp()
    {
        /*
        GameObject.FindObjectOfType<InventorySystem>().endSlot = id;
        */
    }
}
