using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class EventData
{
    public int Id;
    public string Title;
    public string Lore;

    public EventType Type = EventType.Random;
    public EventRarity Rarity = EventRarity.Random;
    public EventGoodness Goodness = EventGoodness.Random;

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
    public string Feedback;

    public EventType Type = EventType.Random;
    public EventRarity Rarity = EventRarity.Random;
    public EventGoodness Goodness = EventGoodness.Random;

    public bool Force;

    public float Time;
    public float Morale;
    public int Target;

    public int Wood;
    public int Meat;
    public int Berries;
    public int MedicinalHerbs;
}

public enum EventType
{
    Campement,
    FindCampement,
    Ressource,
    Random,
    Special,
    Target
}

public enum EventRarity
{
    Often,
    Regular,
    Rare,
    Random
}

public enum EventGoodness
{
    Good,
    Neutral,
    Bad,
    Terrible,
    Random
}