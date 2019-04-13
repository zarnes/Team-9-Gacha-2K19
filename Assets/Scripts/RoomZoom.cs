using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomZoom : MonoBehaviour
{
    public Transform Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StopAllCoroutines();
            StartCoroutine(ScaleUp(5));
        }
    }

    public IEnumerator ScaleUp(float duration)
    {
        float timeStart = Time.time;
        float timeCurrent = timeStart;
        float timeEnd = timeStart + duration;

        Vector3 direction = Player.position - transform.position;
        transform.localScale = Vector3.one;

        while (transform.localScale.x < 10)
        {
            timeCurrent += Time.deltaTime;
            float percentage = Mathf.InverseLerp(timeStart, timeEnd, timeCurrent);
            float scale = Mathf.Lerp(1, 10, percentage);
            transform.localScale = Vector3.one * scale;

            Player.Translate(direction * Time.deltaTime);

            yield return null;
        }
    }
}
