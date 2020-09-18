using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomController : MonoBehaviour
{
    public Camera cam;
    private float targetZoom;
    public float zoomFactor;
    public float minZoom;
    public float maxZoom;
    public float zoomLerpSpeed;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        targetZoom = cam.orthographicSize;

        // Target zoom can be set to start 
         targetZoom = maxZoom;
    }

    // Update is called once per frame
    void Update()
    {
        float getAxis;
        getAxis = Input.GetAxis("Mouse ScrollWheel"); // use mouse wheel to zoom in and out

        targetZoom -= getAxis * zoomFactor;
        targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom); // clamped so that image is not inverted
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);
    }
}
