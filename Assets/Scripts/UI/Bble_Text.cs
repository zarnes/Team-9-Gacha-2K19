using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bble_Text : MonoBehaviour
{
    public Text txtRef;
    public float rate = 0.1f;

    private int index = 0;
    private bool isCompleted = false;
    IEnumerator displayTextInterraction;

    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    public void Setup(string txt) {

        if (displayTextInterraction != null)
        {
            StopCoroutine(displayTextInterraction);
            txtRef.text = "";
            index = 0;
        }
        displayTextInterraction = DisplayTextWithOutInterraction(txt);
        StartCoroutine(displayTextInterraction);
    }

    IEnumerator DisplayTextWithOutInterraction(string txt)
    {
        int lenght = txt.Length;
        txtRef.text = "";

        for (int i = 0; i < lenght; i++)
        {
            yield return new WaitForSeconds(rate);
            txtRef.text += txt[i];
        }

        yield return new WaitForSeconds(Random.Range(0, 10f));

        txtRef.text = "";
        index = 0;
        this.gameObject.SetActive(false);

    }
}
