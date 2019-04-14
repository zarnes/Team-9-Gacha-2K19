using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowerBehavior : NpcMove
{

    public bool isPossessed;
    [HideInInspector] public FPSController fpsController;

    protected override void Start() {
        if (isPossessed) {
            Possess();
        } else {
            Init();
        }
    }

    protected override void Update() {
        if (_navMeshAgent == null)
            _navMeshAgent = GetComponent<NavMeshAgent>();
        if (!isPossessed) {
            GetDistance();
            if (distance < minimal_Distance) {
                _navMeshAgent.speed = 0;
            } else {
                SetDestination();
                _navMeshAgent.speed = 3.5f;
            }
        }
    }

    public void Possess() {
        isPossessed = true;
        Camera.main.transform.parent = this.transform;
        Camera.main.transform.localPosition = new Vector3(0f, 1f, 0.5f);
        this.GetComponent<FPSController>().enabled = true;
        this.GetComponent<Rigidbody>().isKinematic = true;
        this.GetComponent<NavMeshAgent>().enabled = false;
        _destination = null;
    }

    public void Unpossess(Transform follow) {
        isPossessed = false;
        this.GetComponent<FPSController>().enabled = false;
        this.GetComponent<NavMeshAgent>().enabled = true;
        this.GetComponent<Rigidbody>().isKinematic = true;
        _destination = follow;
    }
}
