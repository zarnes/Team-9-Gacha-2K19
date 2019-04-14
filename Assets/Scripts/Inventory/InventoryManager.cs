using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Slot selectedSlot = null;
    public Image selectionIcon;
    
    public void OnSlotClicked(Slot clickedSlot)
    {
        if (selectedSlot == null) //get
        {
            selectedSlot = clickedSlot;
            selectionIcon.sprite = clickedSlot.item.getIcon();
        }
        else
        {
            if (clickedSlot.item == null) //drop
            {
                clickedSlot.item = selectedSlot.item;
                selectedSlot.item = null;
                selectedSlot.RefreshUI();
                selectedSlot = null;
                selectionIcon.sprite = null;
            }
            else //swap
            {
                Item itemTemp = clickedSlot.item;
                clickedSlot.item = selectedSlot.item;
                selectedSlot.item = itemTemp;
                selectedSlot.RefreshUI();
                selectedSlot = clickedSlot;
                selectionIcon.sprite = selectedSlot.item.getIcon();
            }
        }
        clickedSlot.RefreshUI();
    }
}
