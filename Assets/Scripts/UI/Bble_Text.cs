using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bble_Text : MonoBehaviour
{
    public List<string> dialogues = new List<string>();
    public Text txtRef;
    public float rate = 0.1f;

    private int index = 0;
    private bool isCompleted = false;
    IEnumerator displaytext;

    /*
    private void Start()
    {
        Setup(txtRef.text);
    }
    */

    private void OnEnable()
    {
        Setup(dialogues);
    }

    private void Update()
    {
        if (Input.anyKeyDown && isCompleted)
        {
            Setup(dialogues);
        }
    }

    public void Setup(List<string> listDialogues) {

        if(index < dialogues.Count)
        {
            displaytext = DisplayText(listDialogues[index]);
            StartCoroutine(displaytext);
            
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

        for (int i = 0;i < lenght;i++)
        {
            yield return new WaitForSeconds(rate);
            txtRef.text += txt[i];
        }
        index++;
        isCompleted = true;
    }
}
