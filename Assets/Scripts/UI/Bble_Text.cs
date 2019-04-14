using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bble_Text : MonoBehaviour
{
    public Text txtRef;

    public void Setup(string txt) {
        txtRef.text = txt;
    }
}
