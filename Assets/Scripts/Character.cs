using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterInfo m_characterInfo;
    public CharacterInventory m_inventory;
    [SerializeField]
    protected bool isDead;

    public void SetDead(bool dead)
    {
        isDead = dead;
    }

    public bool IsDead()
    {
        return isDead;
    }
}
