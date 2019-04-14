using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject floor;

    public int woodToSpawnMax, foodToSpawnMax;

    public GameObject food, wood;
    public int woodToSpawn, foodToSpawn;

    public int StageNumber;
    public GameObject mother;
    public GameObject[] childrens;


    /*public List<GameObject> wood = new List<GameObject>();
    public List<GameObject> food = new List<GameObject>();*/

    // Start is called before the first frame update
    void Start()
    {
        mother = GameObject.Find("Mother");
        mother.SetActive(false);
        childrens = GameObject.FindGameObjectsWithTag("Player");
        mother.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            StageNumber++;
            Generate();
        }

        if(StageNumber >=5)
        {
            //Victory Condition
        }

        if(childrens.Length == 0)
        {
            //Loose Condition
            Debug.Log("You lose");
        }
    }

    void Generate()
    {
        woodToSpawn = Random.Range(1, woodToSpawnMax);
        for (int a = 0; a < woodToSpawn; a++)
        {
            var xPlane = Random.Range(-floor.GetComponent<Collider>().bounds.size.x / 2, floor.GetComponent<Collider>().bounds.size.x / 2);
            var zPlane = Random.Range(-floor.GetComponent<Collider>().bounds.size.z / 2, floor.GetComponent<Collider>().bounds.size.z / 2);
            Vector3 positionToSpawn = new Vector3(xPlane,wood.transform.position.y, zPlane);
            //Vector3 positionToSpawn = Random.onUnitSphere * 25;
            //Debug.Log(positionToSpawn);
            Instantiate(wood, positionToSpawn, Quaternion.identity);
        }

        foodToSpawn = Random.Range(1, foodToSpawnMax);
        for (int a = 0; a < foodToSpawn; a++)
        {
            var xPlane = Random.Range(-floor.GetComponent<Collider>().bounds.size.x / 2, floor.GetComponent<Collider>().bounds.size.x / 2);
            var zPlane = Random.Range(-floor.GetComponent<Collider>().bounds.size.z / 2, floor.GetComponent<Collider>().bounds.size.z / 2);
            Vector3 positionToSpawn = new Vector3(xPlane, food.transform.position.y, zPlane);
            //Debug.Log(positionToSpawn);
            Instantiate(food, positionToSpawn, Quaternion.identity);
        }
    }
}
