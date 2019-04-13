using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowersUIManager : MonoBehaviour
{
    public int selected = 0;

    public List<GameObject> followersUI = new List<GameObject>();
    public GameObject followersParent;



    private void Update()
    {
        RefreshUI();
    }

    void RefreshUI()
    {
        for(int i =0; i< followersParent.transform.childCount;i++)
        {
            if (i == selected)
                followersParent.transform.GetChild(i).transform.localScale = Vector3.one * 1.2f;
            else
                followersParent.transform.GetChild(i).transform.localScale = Vector3.one;
        }
    }

}
