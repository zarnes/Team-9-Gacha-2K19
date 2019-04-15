using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject MainMenu, OptionMenu, CreditsMenu;
    public string m_gameSceneName;

    IEnumerator StartGameScene() 
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(m_gameSceneName);
        while (!asyncOperation.isDone) {
            //m_asyncOperationProgressText.text = "Loading progress: " + (asyncOperation.progress * 100) + "%";
            yield return new WaitForEndOfFrame();
        }
    }

    public void OnPlayButtonClicked() {
        StartCoroutine(StartGameScene());
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

    public void StopApplication() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
         Application.OpenURL(webplayerQuitURL);
#else
         Application.Quit();
#endif
    }
}
