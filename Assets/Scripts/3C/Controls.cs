using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Controls : MonoBehaviour
{
    private NavMeshAgent m_nav_mesh_agent_;
    // Start is called before the first frame update
    void Start()
    {
        InputManager.GetInstance().OnClickLeftMouseButton += OnMouseHandler;
        m_nav_mesh_agent_ = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //m_nav_mesh_agent_.SetDestination()
    }

    private void OnMouseHandler()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000))
            m_nav_mesh_agent_.SetDestination(hit.point);
    }

    private void OnDestroy()
    {
        InputManager.GetInstance().OnClickLeftMouseButton-= OnMouseHandler;
    }
}
