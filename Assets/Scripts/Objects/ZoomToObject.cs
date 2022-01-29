using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ZoomToObject : MonoBehaviour
{
    private Camera cam;
    [SerializeField]
    Timer timer;

    [SerializeField]
    float zoomSpeed = 3;

    Transform objectPos;

    [SerializeField]
    float lerpTime;

    //Vector3 originalPos;
    float originalSize, startTime = 4;
    void Start()
    {
        cam = Camera.main;
        objectPos = gameObject.transform.GetChild(gameObject.GetComponent<ItemSelector>().getObjectId());
        originalSize = cam.orthographicSize - 3.5f;
        //originalPos = cam.transform.position;
    }

    void Update()
    {
        if (startTime > 1)
        {
            //Si ha perdido
            startTime -= Time.deltaTime;

            string seconds = Math.Truncate((startTime % 60)).ToString();
        }
        else
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, originalSize, zoomSpeed * Time.deltaTime);
            cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(objectPos.position.x, objectPos.position.y, cam.transform.position.z), lerpTime);
            if (cam.orthographicSize <= originalSize + 0.1)
                timer.closeTelon();
        }
        
    }
}
