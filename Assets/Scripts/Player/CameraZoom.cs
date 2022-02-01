using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private Camera cam;
    [SerializeField]
    Timer timer;

    [SerializeField]
    float zoomSpeed = 1;

    float originalSize;
    void Start()
    {
        cam = Camera.main;
        originalSize = cam.orthographicSize - 2.5f;
    }

    void Update()
    {
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, originalSize, zoomSpeed * Time.deltaTime);

        if (!timer.ganas)
            timer.ganas = true;
        if (cam.orthographicSize <= originalSize + 0.1)
        {
            timer.closeTelon();
            AudioManager.instance.Play(AudioManager.ESounds.telon);
        }
    }
}
