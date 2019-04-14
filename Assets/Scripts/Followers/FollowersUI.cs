using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class FollowersUI : MonoBehaviour
{
    public Text textName;
    public Text textConditions;
    public Image imageIcone;



    private void Start()
    {
        if (textName == null)
            textName = transform.Find("Text_Name").GetComponent<Text>();

        if (textConditions == null)
            textConditions = transform.Find("Text_Conditions").GetComponent<Text>();



        DefineUI();

    }

    private void DefineUI()
    {
        textName.text = "nomFamilier";
        textConditions.text = "conditionFamilier";
    }
}
