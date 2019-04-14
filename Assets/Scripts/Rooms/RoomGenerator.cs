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
            Generate(Vector3.zero);
        }
    }

    public void Generate(Vector3 position)
    {
        Instantiate(RoomPrefab);
    }
}
