using System.Collections;
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
    private GameObject m_fire_camp;
    
    private GameObject m_destination_pickup_;
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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask("Ground");
        if (Physics.Raycast(ray, out hit, 1000, mask))
            m_nav_mesh_agent.SetDestination(hit.point);
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
            m_destination_pickup_ = Item;
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetHashCode() == m_destination_pickup_.GetHashCode())
            ComeBackBackToCamp();
    }

    private void ComeBackBackToCamp()
    {
        Vector3 RandomPositionComeBack = Random.insideUnitSphere;
        RandomPositionComeBack += m_fire_camp.transform.position;
        m_nav_mesh_agent.SetDestination(RandomPositionComeBack);
    }
}