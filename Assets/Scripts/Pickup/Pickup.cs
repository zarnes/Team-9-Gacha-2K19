using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Item m_item;

    private SpriteRenderer m_sprite_renderer_; 
    // Start is called before the first frame update
    void Start()
    {
        m_sprite_renderer_ = GetComponent<SpriteRenderer>();
        m_sprite_renderer_.sprite = m_item.icon;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
