using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour, ISlotListener, IPointerClickHandler
{
    public Image m_image;

    [Header("Debug")]
    [SerializeField]
    protected Slot m_slot = null;
    public Image m_bcg;
    public bool IsDisabled = false;

    public void Init(Slot _slot)
    {
        Invalidate();
        _slot.AddListener(this);
        m_slot = _slot;
        RefreshUI();
    }

    public void OnSlotChange()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        if (!m_slot.IsEmpty())
        {
            m_image.sprite = m_slot.GetItem().getIcon();
        }
        else
        {
            m_image.sprite = null;
        }
    }

    private void Invalidate()
    {
        if (m_slot != null)
        {
            m_slot.RemoveListener(this);
        }
        m_slot = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!IsDisabled)
        {
            m_slot.GetInventory().OnSlotClicked(m_slot);
        }
    }
}
