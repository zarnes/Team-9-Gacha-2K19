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

        if (ChoicePrefab == null || ViewPortContent == null || OtherScene == "")
        {
            Debug.LogError("Can't load events, missing attributes", gameObject);
            return;
        }

        _animator = GetComponent<Animator>();

        NextEvent(null, true);
    }

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
            _animator.SetTrigger("Next");
            yield return new WaitForSeconds(0.5f);
        }

        EventData eventData;
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

            eventData = EventsLoader.Instance.GetEvent(data.Type);
        }

        if (eventData == null)
        {
            Debug.LogError("No event found");
            yield break;
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
            }
            else
            {
                panel.Find("Food Gauge Mask/Image").GetComponent<Image>().fillAmount = fill;
            }
        }

        transform.Find("Moral/Morale Gauge Mask/Image").GetComponent<Image>().fillAmount = CharactersData.Morale;
    }
}
