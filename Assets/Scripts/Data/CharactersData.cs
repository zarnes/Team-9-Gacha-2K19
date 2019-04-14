using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharactersData
{
    public static List<CharacterData> Characters = new List<CharacterData>();
    public static float TimeLeft;
    public static float MaxTimeLeft = 300;
    public static float Morale;

    static CharactersData()
    {
        Characters.Add(new CharacterData("Batman"));
        Characters.Add(new CharacterData("Superman"));
        Characters.Add(new CharacterData("Ton chien"));
        Characters.Add(new CharacterData("Le voisin"));
        Characters.Add(new CharacterData("Gargamel"));

        TimeLeft = 120;
        Morale = 100;
    }

    public static bool AddTime (float time)
    {
        TimeLeft += time;

        if (TimeLeft < 0)
        {
            TimeLeft = 0;
            return false;
        }
        else if (TimeLeft > MaxTimeLeft)
        {
            TimeLeft = MaxTimeLeft;
            return false;
        }

        return false;
    }
}

public class CharacterData
{
    public string Name;
    public float Speed;
    public float Food;
    public CharacterState State;
    
    public CharacterData(string name, float speed = 10, float food = 3, CharacterState state = CharacterState.Good)
    {
        Name = name;
        Speed = speed;
        Food = food;
    }
}

public enum CharacterState
{
    Good,
    Injured,
    Dieded
}