using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireCheckSecureZone : MonoBehaviour
{
    public MapGenerator mapGeneratorScript;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Wood")
        {
            Destroy(other.gameObject);
            mapGeneratorScript.woodToSpawn++;
        }
        else if (other.gameObject.tag == "Food")
        {
            Destroy(other.gameObject);
            mapGeneratorScript.foodToSpawn++;
        }
    }

}
