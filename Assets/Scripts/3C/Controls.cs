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
    private Pickup m_destination_item_;
    private GameObject m_current_selected_object_;
    private CharacterInventory m_charactere_inventory_;
    
    public Animator Character_Animator;
    private Vector3 scale;

    public CharacterState stateCharacter;
    // Start is called before the first frame update
    private void Awake()
    {
        scale = transform.localScale;
        if(gameObject.name != "Mother")
            Character_Animator = GetComponent<Animator>();
        InputManager.GetInstance().OnClickRightMouseButton += OnClickRightMouseHandler;
      
        stateCharacter = CharactersData.Characters.Find(character => character.Name == name).State;
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.z * 100f) * -1;

        if (Character_Animator != null)
        {
            if (m_nav_mesh_agent.velocity.magnitude > 0.5f)
            {
                Character_Animator.SetBool("Walk", true);
            }
            else
                Character_Animator.SetBool("Walk", false);
        }

        if (stateCharacter == CharacterState.Injured)
        {
            if (Character_Animator != null)
                Character_Animator.SetBool("Injured", true);
        }
        else if (stateCharacter == CharacterState.Good)
        {
            if (Character_Animator != null)
                Character_Animator.SetBool("Injured", false);
        }
        else
        {
            var isDeadInst = GetComponent<Character>().IsDead();
            isDeadInst = true;
            if (isDeadInst)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnClickRightMouseHandler()
    {
        if(m_current_selected_object_ != null)
        {
            var shader = m_current_selected_object_.GetComponent<Renderer>().material;
            shader.SetColor("_OutlineColor", Color.clear);
        }
        var Player = GetGameObjectInRaycastAllByTag("Player");
        if (Player != null && Player.name == this.gameObject.name)
        {
            m_current_selected_object_ = Player;
            var shader = m_current_selected_object_.GetComponent<Renderer>().material;
            shader.SetColor("_OutlineColor", Color.green);
            return;
        }

        // todo
        var Item = GetItem();

        if (m_current_selected_object_ != null && Item != null)
        {
            m_pop_up_confirm.GetComponent<AnchoredSpriteUI>().target = Item.transform;
            m_destination_pickup_ = Item.gameObject;
            m_destination_item_ = Item;
            m_pop_up_confirm.SetActive(true);
            m_pop_up_confirm.GetComponent<Bble_Confirm>().Setup(Item.m_item);
            var shader = m_current_selected_object_.GetComponent<Renderer>().material;
            shader.SetColor("_OutlineColor", Color.clear);
            return;
        }
        this.m_current_selected_object_ = null;
    }

    private void OnDestroy()
    {
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
            PickInInventory(m_destination_item_.m_item);
            ComeBackBackToCamp();
        }
    }

    private void ComeBackBackToCamp()
    {
        m_nav_mesh_agent.SetDestination(m_fire_camp.transform.position);
    }

    public void MoveToDestination()
    {
        if (m_current_selected_object_ != null && m_current_selected_object_.CompareTag("Player") && m_destination_pickup_ != null)
        {
            m_nav_mesh_agent.SetDestination(m_destination_pickup_.transform.position);
            if (m_nav_mesh_agent.destination.x < m_current_selected_object_.transform.position.x)
            {
                m_current_selected_object_.transform.localScale = new Vector3(scale.x, scale.y, scale.z);
            }
            else if (m_nav_mesh_agent.destination.x > m_current_selected_object_.transform.position.x)
            {
                m_current_selected_object_.transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
            }
        }              
        m_pop_up_confirm.SetActive(false);
    }

    public void CancelMoveToDestination()
    {
        m_destination_pickup_ = null;
        m_pop_up_confirm.SetActive(false);
        
    }

    private void PickInInventory(Item _item)
    {
        if (m_current_selected_object_ != null && m_current_selected_object_.CompareTag("Player"))
            m_charactere_inventory_ = m_current_selected_object_.GetComponent<CharacterInventory>();
        bool t = m_charactere_inventory_.AddItem(m_destination_item_.m_item);
        if (!t)
            Debug.Log("failed");

    }
}
