using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject floor;

    public int woodToSpawn, foodToSpawn;

    public GameObject food, wood;

    /*public List<GameObject> wood = new List<GameObject>();
    public List<GameObject> food = new List<GameObject>();*/

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            Generate();
        }
    }

    void Generate()
    {

        for (int a = 0; a < woodToSpawn; a++)
        {
            var xPlane = Random.Range(-floor.GetComponent<Collider>().bounds.size.x / 2, floor.GetComponent<Collider>().bounds.size.x / 2);
            var zPlane = Random.Range(-floor.GetComponent<Collider>().bounds.size.z / 2, floor.GetComponent<Collider>().bounds.size.z / 2);
            Vector3 positionToSpawn = new Vector3(xPlane,wood.transform.position.y, zPlane);
            Debug.Log(positionToSpawn);
            Instantiate(wood, positionToSpawn, Quaternion.identity);
        }

        for (int a = 0; a < foodToSpawn; a++)
        {
            var xPlane = Random.Range(-floor.GetComponent<Collider>().bounds.size.x / 2, floor.GetComponent<Collider>().bounds.size.x / 2);
            var zPlane = Random.Range(-floor.GetComponent<Collider>().bounds.size.z / 2, floor.GetComponent<Collider>().bounds.size.z / 2);
            Vector3 positionToSpawn = new Vector3(xPlane, wood.transform.position.y, zPlane);
            Debug.Log(positionToSpawn);
            Instantiate(food, positionToSpawn, Quaternion.identity);
        }
    }
}
