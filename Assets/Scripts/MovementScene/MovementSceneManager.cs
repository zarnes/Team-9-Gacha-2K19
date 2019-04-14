using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MovementSceneManager : MonoBehaviour
{
    public static MovementSceneManager Instance;

    public string OtherScene;
    public Transform ViewPortContent;
    public GameObject ChoicePrefab;
    public Sprite[] Smileys;

    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (ChoicePrefab == null || ViewPortContent == null || OtherScene == "" || Smileys.Length == 0)
        {
            Debug.LogError("Can't load events, missing attributes", gameObject);
            return;
        }

        _animator = GetComponent<Animator>();

        NextEvent(null, true);
    }

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            CharactersData.Morale += 10;
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            CharactersData.Morale -= 10;

        UpdateValues();
    }*/

    public void ReadEvent(EventData data)
    {
        CleanChoices();

        ViewPortContent.transform.Find("Title").GetComponent<Text>().text = data.Title;
        ViewPortContent.transform.Find("Lore").GetComponent<Text>().text = data.Lore;

        foreach(ChoiceData choice in data.Choices.Values)
        {
            GameObject choiceGo = Instantiate(ChoicePrefab, ViewPortContent.transform);
            choiceGo.GetComponent<MovementChoice>().Init(choice);
        }
    }

    public void Choose(ChoiceData data)
    {
        CleanChoices();
        ViewPortContent.transform.Find("Lore").GetComponent<Text>().text = data.Feedback;

        GameObject choiceGo = Instantiate(ChoicePrefab, ViewPortContent.transform);
        choiceGo.GetComponent<MovementChoice>().Init(data, true);
    }

    public void NextEvent(ChoiceData data = null, bool noAnim = false)
    {
        UpdateValues();
        StartCoroutine(INextEvent(data, noAnim));
    }

    private IEnumerator INextEvent(ChoiceData data = null, bool noAnim = false)
    {
        if (!noAnim)
        {
            if (data != null && data.Type != EventType.Campement)
                _animator.SetTrigger("Next");
            yield return new WaitForSeconds(0.5f);
        }

        EventData eventData = null;
        if (data == null)
        {
            eventData = EventsLoader.Instance.GetEvent();
        }
        else
        {
            if (data.Type == EventType.Campement)
            {
                GetComponent<Animator>().SetTrigger("Quit");
                yield break;
            }
            else if (data.Type == EventType.Target)
            {
                eventData = EventsLoader.Instance.GetEvent(data.Target);
                if (eventData == null)
                {
                    Debug.LogError("Can't find target event " + data.Target);
                    eventData = EventsLoader.Instance.GetEvent();
                }
            }
            else
            {
                eventData = EventsLoader.Instance.GetEvent(data.Type, data.Rarity, data.Goodness);
            }
            
        }

        if (eventData == null)
        {
            Debug.LogError("No event found");
            eventData = EventsLoader.Instance.GetEvent();
        }

        ReadEvent(eventData);
    }

    public void SwitchScene()
    {
        SceneManager.LoadScene(OtherScene);
    }

    private void CleanChoices()
    {
        foreach (Transform child in ViewPortContent.transform)
        {
            if (child.parent == ViewPortContent.transform && child.name.Contains("Action"))
                Destroy(child.gameObject);
        }
    }

    private void UpdateValues()
    {
        for (int i = 0; i < 5; ++i)
        {
            Transform panel = transform.Find("Children/" + (i + 1));
            CharacterData child = CharactersData.Characters[i];

            panel.Find("Name").GetComponent<Text>().text = child.Name;
            float fill = child.Food / child.FoodMax;
            if (child.State == CharacterState.Dieded)
            {
                panel.Find("Food Gauge Mask").gameObject.SetActive(false);
                panel.Find("Food Icon").gameObject.SetActive(false);
                panel.Find("Skull").gameObject.SetActive(true);
            }
            else
            {
                panel.Find("Food Gauge Mask/Image").GetComponent<Image>().fillAmount = fill;
            }
        }

        float moral = CharactersData.Morale;
        Sprite smiley;
        transform.Find("Moral/Morale Gauge Mask/Image").GetComponent<Image>().fillAmount = moral / 100;
        if (moral < 20)
            smiley = Smileys[0];
        else if (moral < 40)
            smiley = Smileys[1];
        else if (moral < 60)
            smiley = Smileys[2];
        else if (moral < 80)
            smiley = Smileys[3];
        else if (moral < 100)
            smiley = Smileys[4];
        else
            smiley = Smileys[5];

        transform.Find("Moral/Smiley").GetComponent<Image>().sprite = smiley;
    }
}
