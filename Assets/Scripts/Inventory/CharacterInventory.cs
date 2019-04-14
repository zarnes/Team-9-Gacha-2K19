using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInventory : Inventory
{
    public List<Slot> slots;
    public GameObject slotObject;
    public Vector2 inventoryPosition;
    public int gridWidth = 0, gridHeight = 0;
    public float slotWidth, slotOffset;

    public int startSlot = -1;
    public int endSlot = -1;

    public int inventorySelected = 0;

    public bool isDraging = false;

    public Image dragItem;

   
    private Slot[] slotsItems;

    private void Start()
    {
        foreach (Slot slot in slots)
        {
            slot.Init(this);
        }
    }



    private void Update()
    {
        DragItem();

        if(startSlot > 0 && endSlot > 0)
        {
            //SwapItems();
            startSlot = -1;
        }
  
    }

    void DragItem()
    {
        if (isDraging)
        {
            dragItem.enabled = true;
            Vector3 p = Input.mousePosition;
                
            dragItem.transform.position = p;

            if (Input.GetButtonUp("Fire1"))
            {
                dragItem.enabled = false;
                isDraging = false;
            }
        }
    }

    public void SwapItems(Item source, Item destination)
    {
        if (endSlot < 0 || startSlot < 0)
            return;

        Item s = source;
        source = destination;
        destination = s;


    }

    public void AddItem(Item i, Item[,]inventory)
    {
        for(int a = 0; a < gridWidth * gridHeight; a++)
        {
            if(inventory[a,inventorySelected] == null)
            {
                inventory[a, inventorySelected] = i;
                return;
            }
        }
    }

    public void AddItemAt(Item i, Item[,] inventory)
    {
        Item end = inventory[startSlot, inventorySelected];

        inventory[endSlot, inventorySelected] = i;
        inventory[startSlot, inventorySelected] = end;

    }

    public void RemoveItem(int index, Item[] inventory)
    {
        inventory[index] = null;
    }

    public void DropItem(int index, Item[] inventory)
    {
        inventory[index] = null;

    }



}
