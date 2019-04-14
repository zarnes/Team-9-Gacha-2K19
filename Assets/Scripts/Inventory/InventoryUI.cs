using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Character m_character;
    public List<SlotUI> m_slotsUI;

    void Start()
    {
        if (m_character == null)
        {
            Debug.LogError("Character cannot be null", gameObject);
        }
        List<Slot> slots = m_character.m_inventory.slots;
        if (slots.Count != m_slotsUI.Count)
        {
            Debug.LogError("Inventory size doesn't fit with inventory UI", gameObject);
        }
        for(int i = 0; i < slots.Count; i++)
        {
            m_slotsUI[i].Init(slots[i]);
        }
    }
}
