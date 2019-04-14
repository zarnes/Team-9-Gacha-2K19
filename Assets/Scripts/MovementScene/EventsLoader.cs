using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class EventsLoader : MonoBehaviour
{
    public static EventsLoader Instance;

    private List<EventData> _events;
    private System.Random _rnd = new System.Random();

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        Instance = this;
        LoadEvents();
    }

    private void LoadEvents()
    {
        using (var reader = new StreamReader(Path.Combine(Application.streamingAssetsPath, "Events.xml")))
        {
            XmlSerializer deserializer = new XmlSerializer(
                typeof(EventListData), new XmlRootAttribute("Events"));
            _events = (deserializer.Deserialize(reader) as EventListData).Values;
        }
    }

    public EventData GetEvent(EventType type)
    {
        List<EventData> compatibleEvents = new List<EventData>();
        if (type == EventType.Random)
        {
            compatibleEvents = _events.FindAll(e => e.Type != EventType.Special);
        }
        else
        {
            compatibleEvents = _events.FindAll(e => e.Type == type);
        }
        
        if (compatibleEvents.Count == 0)
            return null;

        int index = _rnd.Next(compatibleEvents.Count);
        return compatibleEvents[index];
    }

    public EventData GetEvent(int id)
    {
        return _events.Find(e => e.Id == id);
    }
}
