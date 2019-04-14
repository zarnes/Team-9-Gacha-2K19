using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    private static FPSCounter _instance;

    private Text _text;

    public Color GoodColor = Color.green;
    public Color BadColor = Color.red;
    public float UpdateRate = 10;

    private int _oldFps;
    private int _oldOldFps;

    private void Start()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        _text = GetComponentInChildren<Text>();
        StartCoroutine(UpdateFps());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    // Update is called once per frame
    private IEnumerator UpdateFps()
    {
        Color color = _text.color;
        color.a = 0;
        _text.color = color;
        yield return new WaitForSeconds(0.5f);

        _oldFps = (int)(1f / Time.deltaTime);

        while (true)
        {
            int fps = (int)(1f / Time.deltaTime);
            fps = (int) Mathf.Lerp(_oldFps, fps, 0.2f);
            _text.text = fps + " FPS";
            _text.color = fps >= 60 ? GoodColor : BadColor;

            _oldFps = fps;

            yield return new WaitForSeconds(1f / UpdateRate);
        }
    }
}
