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
        Characters.Add(new CharacterData("Katérina"));
        Characters.Add(new CharacterData("Anna"));
        Characters.Add(new CharacterData("Marina"));
        Characters.Add(new CharacterData("Tatiana"));
        Characters.Add(new CharacterData("Véra"));

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
    public float FoodMax = 5;
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