using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : Inventory
{
    [SerializeField]
    public List<Slot> slots;
    public bool test;

    private void Start()
    {
        foreach(Slot slot in slots)
        {
            slot.SetInventory(this);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            AddItem(new WoodItem(Item.Type.WOOD, "Wood", false, 3));
        }
    }

    public bool AddItem(Item _item, int _quantity = 1)
    {
        foreach (Slot slot in slots)
        {
            if (slot.IsEmpty())
            {
                slot.SetItem(_item, _quantity);
                return true;
            }
            else if (slot.GetItem().name == _item.name)
            {
                slot.Add();
                return true;
            }
        }
        return false;
    }
}
