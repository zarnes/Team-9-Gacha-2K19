using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public Text m_asyncOperationProgressText;
    public string m_gameSceneName;

    void Start()
    {
        if (!Validate())
            StopApplication();
    }

    protected bool Validate()
    {
        bool result = true;
        if (string.IsNullOrEmpty(m_gameSceneName))
        {
            Debug.LogError("[MainMenuManager] Game Scene Name cannot be null or empty.");
            result = false;
        }
        if (m_asyncOperationProgressText == null)
        {
            Debug.LogError("[MainMenuManager] Async Operation Progress Text cannot be null.");
            result = false;
        }
        return result;
    }

    public void OnPlayButtonClicked()
    {
        StartCoroutine(StartGameScene());
    }

    public void OnQuitButtonClicked()
    {
        StopApplication();
    }

    public void OnCreditButtonClicked()
    {
        Debug.Log("Open Credit Scene");
    }

    protected void StopApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
         Application.OpenURL(webplayerQuitURL);
#else
         Application.Quit();
#endif
    }

    IEnumerator StartGameScene()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(m_gameSceneName);
        while (!asyncOperation.isDone)
        {
            m_asyncOperationProgressText.text = "Loading progress: " + (asyncOperation.progress * 100) + "%";
            yield return new WaitForEndOfFrame();
        }
    }
}
