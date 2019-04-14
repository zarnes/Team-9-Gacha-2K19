using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Controls : MonoBehaviour
{
    private NavMeshAgent m_nav_mesh_agent_;

    private GameObject m_current_selected_object_;
    // Start is called before the first frame update
    void Start()
    {
        InputManager.GetInstance().OnClickLeftMouseButton += OnClickLeftMouseHandler;
        InputManager.GetInstance().OnClickRightMouseButton += OnClickRightMouseHandler;
        m_nav_mesh_agent_ = GetComponent<NavMeshAgent>();
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
            m_nav_mesh_agent_.SetDestination(hit.point);
    }

    private void OnClickRightMouseHandler()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000))
        this.m_current_selected_object_ = hit.transform.gameObject;
    }

    private void OnDestroy()
    {
        InputManager.GetInstance().OnClickLeftMouseButton -= OnClickLeftMouseHandler;
        InputManager.GetInstance().OnClickRightMouseButton -= OnClickRightMouseHandler;

    }
}
