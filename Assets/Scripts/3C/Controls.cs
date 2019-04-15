﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

public class Controls : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent m_nav_mesh_agent;

    [SerializeField]
    private Camera m_main_camera;

    private GameObject m_current_selected_object_;

    public Animator Character_Animator;
    private Vector3 scale;

    // Start is called before the first frame update
    void Start()
    {
        scale = transform.localScale;
        Character_Animator = GetComponent<Animator>();
        InputManager.GetInstance().OnClickLeftMouseButton += OnClickLeftMouseHandler;
        InputManager.GetInstance().OnClickRightMouseButton += OnClickRightMouseHandler;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.z * 100f) * -1;

        if (Character_Animator != null)
        {
            if (m_nav_mesh_agent.velocity != Vector3.zero)
            {
                Character_Animator.SetBool("Walk", true);
            }
            else
                Character_Animator.SetBool("Walk", false);
        }
    }

    private void OnClickLeftMouseHandler()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask("Ground");
        if (Physics.Raycast(ray, out hit, 1000, mask))
        {
            m_nav_mesh_agent.SetDestination(hit.point);
            if(m_nav_mesh_agent.destination.x < transform.position.x)
            {
                Debug.Log("Face Left");
                transform.localScale = new Vector3(scale.x,scale.y,scale.z);
            }
            else if(m_nav_mesh_agent.destination.x > transform.position.x)
            {
                Debug.Log("Face Right");
                transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
            }

        }
            

    }

    private void OnClickRightMouseHandler()
    {
        var Player = GetGameObjectInRaycastAllByTag("Player");
        if (Player != null)
        {
            m_current_selected_object_ = Player;
            return;
        }

        var Item = GetGameObjectInRaycastAllByTag("Food");
        if (Item == null)
            Item = GetGameObjectInRaycastAllByTag("Wood");
        if (m_current_selected_object_ != null && Item != null)
        {
            // todo : Run ui condition
            m_nav_mesh_agent.SetDestination(Item.transform.position);
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
}
