using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveOnClick : MonoBehaviour
{
    private Camera _cam;
    private NavMeshAgent _agent;

    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
        _agent = GetComponent<NavMeshAgent>();
        InputManager.GetInstance().OnClickLeftMouseButton += () => { MovePlayer(); };
    }

    void MovePlayer()
    {
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            _agent.SetDestination(hit.point);
        }
    }

}
