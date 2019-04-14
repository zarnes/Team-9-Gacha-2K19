using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bble_Text : MonoBehaviour
{
    public Text txtRef;
    public float rate = 0.1f;

    IEnumerator displaytext;

    public void Setup(string txt) {
        txtRef.text = txt;
    }

    IEnumerator DisplayText(string txt)
    {
        int lenght = txt.Length;
      //  string displayedString

        for (int i = 0;i < lenght;i++)
        {
            yield return new WaitForSeconds(rate);
        }
    }
}
