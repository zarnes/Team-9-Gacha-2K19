using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Watching : MonoBehaviour
{
    public LayerMask LayerMask;

    public UnityEvent OnSee;
    public UnityEvent OnUnsee;
    
    
    private void OnTriggerEnter(Collider other)
    {
        if ((LayerMask & 1 << other.gameObject.layer) == 1 << other.gameObject.layer)
        {
            Debug.Log("See " + other.name);
            OnSee.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((LayerMask & 1 << other.gameObject.layer) == 1 << other.gameObject.layer)
        {
            OnUnsee.Invoke();
            Debug.Log("Unsee " + other.name);
        }
    }
}
