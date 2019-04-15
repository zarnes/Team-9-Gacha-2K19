using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
    public static DeathManager Instance;

    public GameObject[] DeathPrefabs;
    public string[] Names;
    
    // Start is called before the first frame update
    void Start()
    {
        if (DeathPrefabs.Length != 6)
        {
            Debug.LogError("Death prefabs error");
            gameObject.SetActive(false);
        }

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        if (arg1.name == "ClickOnMoveOk")
        {
            CharactersData.IncreaseStep();
            Debug.Log(CharactersData.Step);
        }
        else if (arg1.name == "MovementScene" && CharactersData.Step >= 5)
        {
            SceneManager.LoadScene("Win");
        }
    }

    public void Die(int index)
    {
        GameObject go = Instantiate(DeathPrefabs[index], transform);
        Destroy(go, 3);

        int dead = CharactersData.Characters.FindAll(c => c.State == CharacterState.Dieded).Count;
        if (dead >= 5)
            SceneManager.LoadScene("Lose");
    }

    public void Die(string name)
    {
        List<string> names = new List<string>(Names);
        int index = names.IndexOf(name);
        if (index == -1)
            return;
        Die(index);
    }
}
