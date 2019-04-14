using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    [SerializeField]
    private Item m_item;


    // Start is called before the first frame update
    void Start()
    {
        if (m_item == null)
            m_item = new WoodItem("Bois", 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
