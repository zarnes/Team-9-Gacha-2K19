using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FollowersUIManager : MonoBehaviour
{
    public int selected = 0;

    public List<GameObject> followersUI = new List<GameObject>();
    public GameObject followersParent;
    public GameObject picker;

    public Button buttonNext;
    public Button buttonPrev;

    private int childCount = 0;


    private void Start()
    {
        childCount = followersParent.transform.childCount;
        buttonNext.onClick.AddListener(SelectNext);
        buttonPrev.onClick.AddListener(SelectPrevious);
    }
    private void Update()
    {
       // RefreshUI();
    }

    private void RefreshUI()
    {

        selected = Mathf.Clamp(selected, 0, childCount - 1);

        for(int i =0; i< childCount; i++)
        {
            if (i == selected)
            {
                //followersParent.transform.GetChild(i).transform.localScale = Vector3.one * 1.2f;
                picker.transform.position = followersParent.transform.GetChild(i).transform.position;
            }
            else
            {
                // followersParent.transform.GetChild(i).transform.localScale = Vector3.one;

            }
        }
    }

    public void SetSelected(int id)
    {
        selected = id;
        RefreshUI();
    }

    public void SelectNext()
    {
        if(selected++ >= childCount) 
            selected = 0;
        else
            selected++;
        RefreshUI();
    }

    public void SelectPrevious()
    {
        if (selected-- < 0)
            selected = childCount - 1;
        else
            selected--;
        RefreshUI();
    }

}
