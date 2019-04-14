using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcMove : MonoBehaviour
{
    public Transform _destination;

    protected NavMeshAgent _navMeshAgent;

    protected float distance;
    public float distance_Shoot, minimal_Distance;

    // Use this for initialization
    protected virtual void Start()
    {
        Init();
    }

    protected void Init() {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();

        if (_navMeshAgent == null) {
            Debug.LogError("The nav mesh agent component is not attached to " + gameObject.name);
        } else {
            SetDestination();
        }
    }

    protected virtual void Update()
    {
        GetDistance();
        if (distance < minimal_Distance)
        {          
            _navMeshAgent.speed = 0;
        }
        else
        {
            SetDestination();
            _navMeshAgent.speed = 3.5f;
        }
            

    }

    protected void SetDestination()
    {
        if (_destination != null)
        {
            Vector3 targetVector = _destination.transform.position;
            _navMeshAgent.SetDestination(targetVector);
        }
    }

    protected void GetDistance()
    {
        distance = Vector3.Distance(_destination.position,transform.position);
        //Debug.Log(distance);
    }
}
