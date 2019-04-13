using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProceduralEventManager : MonoBehaviour
{

    public int eventCountStart = 5;
    public Text eventText;
    public Button checkEventButton;

    public enum eventType { eventA, eventB };
    private List<ProceduralEvent> events = new List<ProceduralEvent>();

    private void Start()
    {
        for(int i =0; i < eventCountStart; i++)
        {
            AddEvent();
        }

        checkEventButton.onClick.AddListener(CheckEvents);
    }

    private void CheckEvents()
    {
        for(int i = 0; i < events.Count; i++)
        {
            switch (events[i].type)
            {
                case eventType.eventA:
                    CompareEventTime(events[i]);
                    break;

                case eventType.eventB:
                    CompareEventTime(events[i]);
                    break;
            }
        }
    }

    private void CompareEventTime(ProceduralEvent ev)
    {
        if(ev.timeActivation <= Time.timeSinceLevelLoad)
        {
            eventText.text = ev.type.ToString();
            events.Remove(ev);
        }
    }

    public void AddEvent()
    {
        int randomIndex = Random.Range(0, 2);
        eventType newEvent = eventType.eventA;

        switch (randomIndex)
        {
            case 0:
                newEvent = eventType.eventA;
                break;
            case 1:
                newEvent = eventType.eventB;
                break;
        }


        events.Add(new ProceduralEvent(newEvent,Random.Range(0f,10f)));
    }

}
