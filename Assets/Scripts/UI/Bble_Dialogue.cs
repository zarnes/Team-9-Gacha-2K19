using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bble_Dialogue : MonoBehaviour
{

    public List<string> dialogues = new List<string>();
    public Text txtRef;
    public float rate = 0.1f;

    private int index = 0;
    private bool isCompleted = false;
    IEnumerator displayTextInterraction;



    private void OnEnable()
    {
        Setup();
    }

    private void Update()
    {
        if (Input.anyKeyDown && isCompleted)
        {
            Setup();
        }
    }

    public void Setup()
    {
        /*
        if (displayTextInterraction != null)
        {
            StopCoroutine(displayTextInterraction);
            txtRef.text = "";
            index = 0;
        }
        */

        if (index < dialogues.Count)
        {
            displayTextInterraction = DisplayText(dialogues[index]);
            StartCoroutine(displayTextInterraction);

        }
        else
        {
            txtRef.text = "";
            this.gameObject.SetActive(false);
        }
    }

    IEnumerator DisplayText(string txt)
    {
        int lenght = txt.Length;
        txtRef.text = "";
        isCompleted = false;

        for (int i = 0; i < lenght; i++)
        {
            yield return new WaitForSeconds(rate);
            txtRef.text += txt[i];
        }
        index++;
        isCompleted = true;
    }
}
