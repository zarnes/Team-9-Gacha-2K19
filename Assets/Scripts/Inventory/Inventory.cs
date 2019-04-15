using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventoryManager manager;

    public void OnSlotClicked(Slot clickedSlot)
    {
        if (manager == null)
        {
            Debug.LogError("No manager for this inventory", gameObject);
        }
        manager.OnSlotClicked(clickedSlot);
    }
}
