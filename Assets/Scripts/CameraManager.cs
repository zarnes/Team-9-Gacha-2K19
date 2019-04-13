using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public List<Transform> targets;
    public float zoomScale = 1f;

    private Bounds bounds;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        bounds = new Bounds();

        foreach (Transform t in targets)
        {
            bounds.Encapsulate(t.position);
        }

        Zoom();
        Follow();
    }

    void Follow()
    {
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, new Vector3(bounds.center.x, mainCamera.transform.position.y, bounds.center.y),Time.deltaTime*5f);
    }

    void Zoom()
    {
        //mainCamera.fieldOfView = bounds.size.x * zoomScale;
        mainCamera.focalLength = bounds.size.x * zoomScale;

    }
}
