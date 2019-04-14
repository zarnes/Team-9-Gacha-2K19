using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject MainMenu, OptionMenu, CreditsMenu;
    public void Play()
    {
        SceneManager.LoadScene("ClickOnMoveOk");
    }

    public void Options()
    {
        MainMenu.SetActive(false);
        OptionMenu.SetActive(true);
    }

    public void Credits()
    {
        MainMenu.SetActive(false);
        CreditsMenu.SetActive(true);
    }

    public void Back()
    {
        if (OptionMenu.activeSelf)
        {
            OptionMenu.SetActive(false);
            MainMenu.SetActive(true);
        }

        if (CreditsMenu.activeSelf)
        {
            CreditsMenu.SetActive(false);
            MainMenu.SetActive(true);
        }
    }
}
