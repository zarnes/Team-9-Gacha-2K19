using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Controls : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent m_nav_mesh_agent;

    [SerializeField]
    private Camera m_main_camera;

    private GameObject m_current_selected_object_;
    // Start is called before the first frame update
    void Start()
    {
        InputManager.GetInstance().OnClickLeftMouseButton += OnClickLeftMouseHandler;
        InputManager.GetInstance().OnClickRightMouseButton += OnClickRightMouseHandler;
        //m_nav_mesh_agent_ = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //m_nav_mesh_agent_.SetDestination()
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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    
        LayerMask mask = LayerMask.GetMask("Player");
        var HitsList = Physics.RaycastAll(ray, 1000).ToList();
        var Player = HitsList.Find(p => p.transform.gameObject.tag == "Player");
       

        var Item = HitsList.Find(p => p.transform.gameObject.tag == "Item").transform.gameObject;
        if (this.m_current_selected_object_ != null && Item != null)
        {
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
    
}
