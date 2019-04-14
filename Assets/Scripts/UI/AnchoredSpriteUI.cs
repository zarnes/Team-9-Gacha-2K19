using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchoredSpriteUI : MonoBehaviour
{
    public Transform target;
    public Vector3Int offset;
    private RectTransform rectTrans;

    private void Start() {
        rectTrans = GetComponent<RectTransform>();
    }
    
    void Update() {

        rectTrans.position = Camera.main.WorldToScreenPoint(target.position) + offset;
    }
}
