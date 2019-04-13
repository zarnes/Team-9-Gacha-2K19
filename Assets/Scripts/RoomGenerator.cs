using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public GameObject RoomPrefab;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Generate();
        }
    }

    public void Generate()
    {
        Instantiate(RoomPrefab);
    }
}
