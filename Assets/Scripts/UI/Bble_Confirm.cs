using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bble_Confirm : MonoBehaviour
{
    public Image img;
    public Text txtQty;
    public Text txtDuration;
    public Slider barRisk;

    public void Setup(Sprite spr, string qty, string duration, float percent) {
        img.sprite = spr;
        txtQty.text = qty;
        txtDuration.text = duration;
        barRisk.value = percent;
    }
}
