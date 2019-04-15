using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerCanvas : MonoBehaviour
{
    public float MaxTime = 180;
    public float Remaining;

    private Image _image;
    private bool _quitting;
    private Animator _anim;
    private TimerState _lastState = TimerState.Good;

    public Color Good = Color.green;
    public Color Medium = Color.yellow;
    public Color Bad = Color.red;
    public Color Quit = Color.red;

    public string NextScene;


    // Start is called before the first frame update
    void Start()
    {
        Remaining = MaxTime;
        _image = transform.Find("Timer Mask/Timer").GetComponent<Image>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_quitting)
            return;

        Remaining -= Time.deltaTime;
        float percentage = Remaining / MaxTime;
        _image.fillAmount = percentage;
        TimerState state;

        if (percentage <= 0f)
        {
            _quitting = true;
            _image.color = Quit;
            CharactersData.Chance = ChanceState.VeryUnlucky;
            state = TimerState.Quit;
        }
        else if (percentage <= 0.2f)
        {
            _image.color = Bad;
            CharactersData.Chance = ChanceState.Unlucky;
            state = TimerState.Bad;
        }
        else if (percentage <= 0.5f)
        {
            _image.color = Medium;
            CharactersData.Chance = ChanceState.Neutral;
            state = TimerState.Medium;
        }
        else
        {
            _image.color = Good;
            CharactersData.Chance = ChanceState.Lucky;
            state = TimerState.Good;
        }

        if (_lastState != state)
        {
            if (state == TimerState.Quit)
                _anim.SetTrigger("Quit");
            else
                _anim.SetTrigger("Trigger");

            _lastState = state;
        }
    }

    public void SwitchScene()
    {
        SceneManager.LoadScene(NextScene);
    }
    
    private enum TimerState
    {
        Good,
        Medium,
        Bad,
        Quit
    }
}
