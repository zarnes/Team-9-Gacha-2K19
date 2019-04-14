using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu()]
public class Item : ScriptableObject
{
    public enum Type { WOOD, FOOD }

    public Type type;
    public Sprite icon;
    public string name;
    public bool consumable = false;
    
    public float bakingAmount = 0f;
    public float bakingDuration = 2.0f;

    public Item(Type _type, string _name, bool _consumable)
    {
        type = _type;
        name = _name;
        consumable = _consumable;
    }

    public virtual void Use() 
    {

    }

    public virtual Sprite getIcon()
    {
        return icon;
    }
}

[CreateAssetMenu()]
public class FoodItem : Item
{
    public float value = 0f;
    public bool cooked = false;
    public Sprite cookedImage;
    public FoodItem(Item.Type _type, string _name, bool _consumable, float _value)
    : base(_type, _name, _consumable)
    {
        value = _value;
    }

    public override void Use()
    {
        Debug.Log("using FoodItem");
    }

    public override Sprite getIcon()
    {
        return cooked ? cookedImage : icon;
    }
}

[CreateAssetMenu()]
public class WoodItem : Item
{
    public int carburant = 3;
    public WoodItem(Item.Type _type, string _name, bool _consumable, int _carburant)
    : base(_type, _name, _consumable)

    {
        carburant = _carburant;
    }

    public override void Use()
    {
        Debug.Log("using WoodItem");
    }
}

