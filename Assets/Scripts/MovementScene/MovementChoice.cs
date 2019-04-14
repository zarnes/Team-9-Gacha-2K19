using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementChoice : MonoBehaviour
{
    public ChoiceData Choice { get; private set; }
    private bool _nextEvent;

    public void Init(ChoiceData data, bool nextEvent = false)
    {
        _nextEvent = nextEvent;
        Choice = data;
        GetComponent<Text>().text = nextEvent ? "Continuer" : Choice.Intitulate;
        GetComponent<Button>().interactable = CheckPossibility();
    }

    public void Click()
    {
        if (_nextEvent)
        {
            // Change inventory values
            CharactersData.Morale += Choice.Morale;
            CharactersData.Morale = Mathf.Clamp(CharactersData.Morale, 0, 100);

            //CharacterData.
            
            MovementSceneManager.Instance.NextEvent(Choice);
        }
        else
            MovementSceneManager.Instance.Choose(Choice);
    }

    private bool CheckPossibility()
    {
        if (Choice.Force)
            return true;

        /*if (Choice.Berries < 0 && Player.Inventory.Berries + Choice.Berries < 0)
            return false;*/

        if (CharactersData.TimeLeft + Choice.Time < 0)
            return false;

        if (CharactersData.Morale + Choice.Morale < 0)
            return false;

        return true;
    }
}
