using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Character m_character;
    public List<SlotUI> m_slotsUI;
    public Image m_skullImage;
    public Text m_characterName;
    public Image m_characterPreview;

    void Start()
    {
        if (m_characterName == null)
        {
            Debug.LogError("Character name text cannot be null", gameObject);
        }
        m_characterName.text = m_character.m_characterInfo.m_name;
        if (m_characterPreview == null)
        {
            Debug.LogError("Character preview image cannot be null", gameObject);
        }
        m_characterPreview.sprite = m_character.m_characterInfo.m_sprite;
        if (m_skullImage == null)
        {
            Debug.LogError("Skull image cannot be null", gameObject);
        }
        m_skullImage.gameObject.SetActive(false);
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

    private void Update()
    {
        if (m_character.IsDead() && !m_skullImage.gameObject.activeInHierarchy)
        {
            m_skullImage.gameObject.SetActive(true);
            foreach(SlotUI slot in m_slotsUI)
            {
                slot.IsDisabled = true;
            }
        }
    }
}
