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

    public List<Sprite> objectSpriteToSpawn;
    public int objectSprite;

    /*public List<GameObject> wood = new List<GameObject>();
    public List<GameObject> food = new List<GameObject>();*/

    public int treeToSpawn, treeToSpawnMax, bushToSpawn;

    public List<GameObject> Decor;
    public List<GameObject> Bush;

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
        if (Input.GetKeyDown(KeyCode.M))
        {
            StageNumber++;
            Generate();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            GenerateDecor();
        }

        if (StageNumber >= 5)
        {
            //Victory Condition
        }

        if (childrens.Length == 0)
        {
            //Loose Condition
            Debug.Log("You lose");
        }
    }

    void GenerateDecor()
    {
        //treeToSpawn = Random.Range(1, treeToSpawnMax);
        for (int a = 0; a < treeToSpawnMax; a++)
        {
            var xPlane = Random.Range(-floor.GetComponent<Collider>().bounds.size.x / 2, floor.GetComponent<Collider>().bounds.size.x / 2);
            var zPlane = Random.Range(-floor.GetComponent<Collider>().bounds.size.z / 2, floor.GetComponent<Collider>().bounds.size.z / 2);
            Vector3 positionToSpawn = new Vector3(xPlane, food.transform.position.y, zPlane);

            objectSprite = Random.Range(0, objectSpriteToSpawn.Count);
            var treeToSpawn = Random.Range(0, Decor.Count);
            Instantiate(Decor[treeToSpawn], positionToSpawn, this.transform.rotation);
        }

        for (int a = 0; a < bushToSpawn; a++)
        {
            var xPlane = Random.Range(-floor.GetComponent<Collider>().bounds.size.x / 2, floor.GetComponent<Collider>().bounds.size.x / 2);
            var zPlane = Random.Range(-floor.GetComponent<Collider>().bounds.size.z / 2, floor.GetComponent<Collider>().bounds.size.z / 2);
            Vector3 positionToSpawn = new Vector3(xPlane, food.transform.position.y, zPlane);

            objectSprite = Random.Range(0, objectSpriteToSpawn.Count);
            var bushToSpawn = Random.Range(0, Bush.Count);
            Instantiate(Bush[bushToSpawn], positionToSpawn, this.transform.rotation);
        }
    }

    void Generate()
    {
        foodToSpawn = Random.Range(1, foodToSpawnMax);
        for (int a = 0; a < foodToSpawn; a++)
        {
            var xPlane = Random.Range(-floor.GetComponent<Collider>().bounds.size.x / 2, floor.GetComponent<Collider>().bounds.size.x / 2);
            var zPlane = Random.Range(-floor.GetComponent<Collider>().bounds.size.z / 2, floor.GetComponent<Collider>().bounds.size.z / 2);
            Vector3 positionToSpawn = new Vector3(xPlane, food.transform.position.y, zPlane);

            objectSprite = Random.Range(0, objectSpriteToSpawn.Count);
            Debug.Log(objectSprite);
            GameObject f = Instantiate(food, positionToSpawn, this.transform.rotation);
            f.GetComponent<SpriteRenderer>().sprite = objectSpriteToSpawn[objectSprite];
            Debug.Log(f.GetComponent<SpriteRenderer>().sprite);
        }
    }
}