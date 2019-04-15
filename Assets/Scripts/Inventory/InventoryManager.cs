using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public ItemHandler selectedItem = null;
    public Slot selectedItemSlotOrigin = null;
    public Image selectionIcon;
    public Vector3 offset;

    public void OnSlotClicked(Slot clickedSlot)
    {
        if (selectedItem == null || selectedItem.m_item == null) //get
        {
            if (clickedSlot.IsEmpty())
                return;
            selectedItemSlotOrigin = clickedSlot;
            selectedItem = selectedItemSlotOrigin.GetItemHandler();
            selectedItemSlotOrigin.SetItem(null, 0);
            if (selectionIcon == null)
            {
                Debug.LogError("Inventory Manager doesn't have a selection icon.");
            }
            selectionIcon.sprite = selectedItem.m_item.getIcon();
        }
        else
        {
            if (selectedItem.m_item.type == Item.Type.FOOD && clickedSlot.m_type == Slot.Type.COMBUSTIBLE)
                return;

            if (selectedItem.m_item.type == Item.Type.WOOD && clickedSlot.m_type == Slot.Type.INGREDIENT)
                return;

            if (clickedSlot.IsEmpty()) //drop
            {
                clickedSlot.SetItem(selectedItem.m_item, selectedItem.m_quantity);
                selectedItemSlotOrigin = null;
                selectedItem = null;
                selectionIcon.sprite = null;
            }
            else //swap
            {
                ItemHandler temp = new ItemHandler(clickedSlot.GetItem(), clickedSlot.GetCount());
                clickedSlot.SetItem(selectedItem.m_item, selectedItem.m_quantity);
                selectedItemSlotOrigin = clickedSlot;
                selectedItem = temp;
                selectionIcon.sprite = selectedItem.m_item.getIcon();
            }
        }
    }

    public void Update()
    {
        if (selectionIcon.sprite != null)
        {
            selectionIcon.enabled = true;
            selectionIcon.transform.position = Input.mousePosition + offset;
        }
        else
        {
            selectionIcon.enabled = false;
        }

        if (Input.GetButtonDown("Cancel") && selectedItem != null && selectedItemSlotOrigin != null)
        {
            selectedItemSlotOrigin.SetItem(selectedItem.m_item, selectedItem.m_quantity);
        }
    }
}
