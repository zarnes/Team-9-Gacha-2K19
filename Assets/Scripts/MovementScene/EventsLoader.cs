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

    public EventData GetEventOld(EventType type)
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

    public EventData GetEvent(EventType type = EventType.Random, EventRarity rarity = EventRarity.Random, EventGoodness goodness = EventGoodness.Random, bool force = true)
    {
        List<EventData> compatibleEvents = new List<EventData>(_events);
        compatibleEvents.RemoveAll(e => e.Type == EventType.Special);

        if (type != EventType.Random)
            compatibleEvents.RemoveAll(e => e.Type != type);

        if (rarity != EventRarity.Random)
            compatibleEvents.RemoveAll(e => e.Rarity != rarity);
        else
        {
            int rndRarity = _rnd.Next(9);
            if (rndRarity <= 3)
                compatibleEvents.RemoveAll(e => e.Rarity != EventRarity.Often);
            else if (rndRarity <= 6)
                compatibleEvents.RemoveAll(e => e.Rarity != EventRarity.Regular);
            else
                compatibleEvents.RemoveAll(e => e.Rarity != EventRarity.Rare);
        }

        if (goodness != EventGoodness.Random)
            compatibleEvents.RemoveAll(e => e.Goodness != goodness);
        else
            compatibleEvents.RemoveAll(e => e.Goodness != GetGoodness());

        if (compatibleEvents.Count == 0)
        {
            if (force)
                compatibleEvents = _events;
            else
                return null;
        }

        int index = _rnd.Next(compatibleEvents.Count);
        return compatibleEvents[index];
    }

    public EventData GetEvent(int id)
    {
        return _events.Find(e => e.Id == id);
    }

    private EventGoodness GetGoodness()
    {
        int value;
        switch (CharactersData.Chance)
        {
            case ChanceState.Lucky:
                value = _rnd.Next(400);
                if (value < 30)
                    return EventGoodness.Good;
                if (value < 53)
                    return EventGoodness.Neutral;
                if (value < 76)
                    return EventGoodness.Bad;
                else
                    return EventGoodness.Terrible;
            case ChanceState.Neutral:
                value = _rnd.Next(430);
                if (value < 25)
                    return EventGoodness.Good;
                if (value < 50)
                    return EventGoodness.Neutral;
                if (value < 75)
                    return EventGoodness.Bad;
                else
                    return EventGoodness.Terrible;
            case ChanceState.Unlucky:
                value = _rnd.Next(385);
                if (value < 22)
                    return EventGoodness.Good;
                if (value < 48)
                    return EventGoodness.Neutral;
                if (value < 74)
                    return EventGoodness.Bad;
                else
                    return EventGoodness.Terrible;
            case ChanceState.VeryUnlucky:
                value = _rnd.Next(370);
                if (value < 18)
                    return EventGoodness.Good;
                if (value < 45)
                    return EventGoodness.Neutral;
                if (value < 72)
                    return EventGoodness.Bad;
                else
                    return EventGoodness.Terrible;

        }

        return EventGoodness.Neutral;
    }
}
