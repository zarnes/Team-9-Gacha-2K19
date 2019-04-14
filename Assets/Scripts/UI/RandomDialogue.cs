using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDialogue : MonoBehaviour
{
    public List<string> randomDialogues = new List<string>();
    public Bble_Text textScript;
    IEnumerator dialogue;

    private bool isActive = true;

    void Start()
    {
        if (textScript == null)
            textScript = GameObject.FindObjectOfType<Bble_Text>();


        dialogue = Dialogue();
        StartCoroutine(dialogue);
    }

    IEnumerator Dialogue()
    {
        while (isActive)
        {
            yield return new WaitForSeconds(Random.Range(0f, 20f));
            Debug.Log("perform text");
            textScript.gameObject.SetActive(true);
            int index = Random.Range(0, randomDialogues.Count);
            textScript.Setup(randomDialogues[index]);
        }
    }
}
