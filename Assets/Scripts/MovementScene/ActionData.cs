using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class EventData
{
    public int Id;
    public string Title;
    public string Lore;
    public EventType Type;
    public ChoiceListData Choices;

    public EventData()
    {
        Choices = new ChoiceListData();
    }
}

[XmlRoot("Events")]
public class EventListData
{
    [XmlElement("EventData")]
    public List<EventData> Values;

    public EventListData()
    {
        Values = new List<EventData>();
    }
}

[XmlRoot("Choices")]
public class ChoiceListData
{
    [XmlElement("Choice")]
    public List<ChoiceData> Values;

    public ChoiceListData()
    {
        Values = new List<ChoiceData>();
    }
}

public class ChoiceData
{
    public string Intitulate;
    public EventType Type;
    public string Feedback;

    public int Time;

    public int Wood;
    public int Meat;
    public int Berries;
    public int MedicinalHerbs;
}

public enum EventType
{
    SellChild,
    Campement,
    FindCampement,
    RessourceEvent,
    TimeEvent,
    Random,
    Special,
    Target
}