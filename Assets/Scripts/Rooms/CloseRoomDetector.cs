using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseRoomDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Transform papa = transform.parent;
            papa.GetComponent<Room>().CloseRoom();
        }
    }
}
