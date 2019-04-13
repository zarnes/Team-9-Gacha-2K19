using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralEvent : ProceduralEventManager
{
    public eventType type;
    public float timeActivation = 0f;

    public ProceduralEvent(eventType _type, float _timeActivation)
    {
        type = _type;
        timeActivation = _timeActivation;
    }
}
