using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable, CreateAssetMenu()]
public class CharacterInfo : ScriptableObject
{
    public string m_name;
    public Sprite m_sprite;
    public Sprite m_slot;
    public Sprite m_back;
}
