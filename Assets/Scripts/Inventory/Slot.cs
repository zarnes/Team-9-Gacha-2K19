using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;



[System.Serializable]
public class ItemHandler
{
    public Item m_item;
    public int m_quantity;
    public ItemHandler(Item _item, int _quantity)
    {
        m_item = _item;
        m_quantity = _quantity;
    }
}

[System.Serializable]
public class Slot
{
    public enum Type { ALL, INGREDIENT, COMBUSTIBLE };
    public Type m_type;
    protected Inventory m_inventory;
    [SerializeField]
    protected ItemHandler m_itemHandler;
    protected List<ISlotListener> m_listeners = new List<ISlotListener>();

    public bool IsEmpty()
    {
        return m_itemHandler == null || m_itemHandler.m_item == null;
    }

    public void SetItem([NotNull] Item _item, int _count = 1)
    {
        m_itemHandler = new ItemHandler(_item, _count);

        NotifyListeners();
    }

    public Item GetItem()
    {
        return m_itemHandler?.m_item;
    }

    public void Add()
    {
        if (IsEmpty())
        {
            throw new System.Exception("Slot is empty.");
        }
        m_itemHandler.m_quantity++;
        NotifyListeners();
    }

    public void Add([NotNull] Item _item)
    {
        if (IsEmpty())
        {
            SetItem(_item);
        }
        m_itemHandler.m_quantity++;
        NotifyListeners();
    }
    public void NotifyListeners()
    {
        foreach (ISlotListener listener in m_listeners)
        {
            listener.OnSlotChange();
        }
    }

    public ItemHandler GetItemHandler()
    {
        return m_itemHandler;
    }

    public void Remove()
    {
        if (IsEmpty())
        {
            throw new System.Exception("Slot is empty.");
        }
        m_itemHandler.m_quantity--;
        if (m_itemHandler.m_quantity == 0)
        {
            m_itemHandler = null;
        }
        NotifyListeners();
    }

    public void SetInventory(Inventory _inventory)
    {
        m_inventory = _inventory;
    }

    public Inventory GetInventory()
    {
        return m_inventory;
    }

    public void AddListener(ISlotListener _listener)
    {
        m_listeners.Add(_listener);
    }

    public void RemoveListener(ISlotListener _listener)
    {
        m_listeners.Remove(_listener);
    }

    public int GetCount()
    {
        return m_itemHandler.m_quantity;
    }
}