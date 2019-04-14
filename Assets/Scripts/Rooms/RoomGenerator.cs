using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public GameObject RoomPrefab;

    public GameObject floor;

    public List<GameObject> barrels = new List<GameObject>();
    public List<GameObject> walls = new List<GameObject>();
    public int barrelsToSpawn;
    public int wallsToSpawn;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Generate(Vector3.zero);
        }
    }

    public void Generate(Vector3 position)
    {
        GameObject roomGo = Instantiate(RoomPrefab);
        roomGo.transform.position = position;

        for (int a = 0; a < barrelsToSpawn; a++)
        {
            var barrelsToSpawn = Random.Range(0, barrels.Count);
            Debug.Log(barrelsToSpawn);
            for (int i = 0; i < barrels.Count; i++)
            {
                if (barrelsToSpawn == i)
                {
                    var xPlane = Random.Range(-floor.GetComponent<Collider>().bounds.size.x / 2, floor.GetComponent<Collider>().bounds.size.x / 2);
                    var zPlane = Random.Range(-floor.GetComponent<Collider>().bounds.size.z / 2, floor.GetComponent<Collider>().bounds.size.z / 2);
                    Vector3 positionToSpawn = new Vector3(xPlane, floor.transform.localScale.y, zPlane);
                    Debug.Log(positionToSpawn);
                    Instantiate(barrels[i], positionToSpawn, Quaternion.identity);
                }
            }
        }

        for (int a = 0; a < wallsToSpawn; a++)
        {
            var barrelsToSpawn = Random.Range(0, walls.Count);
            Debug.Log(barrelsToSpawn);
            for (int i = 0; i < walls.Count; i++)
            {
                if (wallsToSpawn == i)
                {
                    var xPlane = Random.Range(-floor.GetComponent<Collider>().bounds.size.x / 2, floor.GetComponent<Collider>().bounds.size.x / 2);
                    var zPlane = Random.Range(-floor.GetComponent<Collider>().bounds.size.z / 2, floor.GetComponent<Collider>().bounds.size.z / 2);
                    Vector3 positionToSpawn = new Vector3(xPlane, floor.transform.localScale.y, zPlane);
                    Debug.Log(positionToSpawn);
                    Instantiate(walls[i], positionToSpawn, Quaternion.identity);
                }
            }
        }
    }
}
