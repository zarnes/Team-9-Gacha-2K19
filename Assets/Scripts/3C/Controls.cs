﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.UIElements;

public class Controls : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent m_nav_mesh_agent;

    [SerializeField]
    private GameObject m_fire_camp;

    [SerializeField]
    private GameObject m_pop_up_confirm;

    private GameObject m_destination_pickup_;
    private Item m_destination_item_;
    private GameObject m_current_selected_object_;

    // Start is called before the first frame update
    void Start()
    {
        InputManager.GetInstance().OnClickLeftMouseButton += OnClickLeftMouseHandler;
        InputManager.GetInstance().OnClickRightMouseButton += OnClickRightMouseHandler;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnClickLeftMouseHandler()
    {
    }

    private void OnClickRightMouseHandler()
    {
        var Player = GetGameObjectInRaycastAllByTag("Player");
        if (Player != null)
        {
            m_current_selected_object_ = Player;
            return;
        }

        // todo 
        var Item = GetItem();

        if (m_current_selected_object_ != null && Item != null)
        {
            m_pop_up_confirm.GetComponent<AnchoredSpriteUI>().target = Item.transform;
            m_destination_pickup_ = Item.gameObject;
            m_pop_up_confirm.SetActive(true);
            return;
        }
        this.m_current_selected_object_ = null;
    }

    private void OnDestroy()
    {
        InputManager.GetInstance().OnClickLeftMouseButton -= OnClickLeftMouseHandler;
        InputManager.GetInstance().OnClickRightMouseButton -= OnClickRightMouseHandler;
    }

    private GameObject GetGameObjectInRaycastAllByTag([NotNull] string _sTag)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] RayCastAll = Physics.RaycastAll(ray, 100);
        for (int i = 0; i < RayCastAll.Length; i++)
        {
            if (RayCastAll[i].transform.gameObject.CompareTag(_sTag))
            {
                return RayCastAll[i].transform.gameObject;
            }
        }
        return null;
    }

    private Pickup GetItem()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] RayCastAll = Physics.RaycastAll(ray, 100);
        Pickup item;
        for (int i = 0; i < RayCastAll.Length; i++)
        {
            item = RayCastAll[i].transform.gameObject.GetComponent<Pickup>();
            if (item != null)
            {
                return item;
            }
        }
        return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_destination_pickup_ != null && other.gameObject.GetHashCode() == m_destination_pickup_.GetHashCode())
        {
            PickInInventory(m_destination_item_);
            ComeBackBackToCamp();
        }
    }

    private void ComeBackBackToCamp()
    {
        m_nav_mesh_agent.SetDestination(m_fire_camp.transform.position);
    }

    public void MoveToDestination()
    {
        if (m_destination_pickup_ != null)
            m_nav_mesh_agent.SetDestination(m_destination_pickup_.transform.position);
        m_pop_up_confirm.SetActive(false);
    }

    public void CancelMoveToDestination()
    {
        m_destination_pickup_ = null;
        m_pop_up_confirm.SetActive(false);
    }

    private void PickInInventory(Item _item)
    {
        this.m_current_selected_object_.GetComponent<CharacterInventory>().slots[0].SetItem(_item);
    }
}