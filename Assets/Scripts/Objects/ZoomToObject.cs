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
    float zoomOutSpeed = 1;

    Transform objectPos;

    [SerializeField]
    float lerpTime;

    //Vector3 originalPos;
    float originalSize, outSize, startTime = 4;
    void Start()
    {
        cam = Camera.main;
        objectPos = gameObject.transform.GetChild(gameObject.GetComponent<ItemSelector>().getObjectId());
        originalSize = cam.orthographicSize - 11f;
        outSize = cam.orthographicSize + 2;
        //originalPos = cam.transform.position;
    }

    void Update()
    {
        if (startTime > 1)
        {
            startTime -= Time.deltaTime;
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, outSize, zoomOutSpeed * Time.deltaTime);
        }
        else
        {
            AudioManager.instance.Play(AudioManager.ESounds.anuncioEscondite);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, originalSize, zoomSpeed * Time.deltaTime);
            cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(objectPos.position.x, objectPos.position.y, cam.transform.position.z), lerpTime);
            if (cam.orthographicSize <= originalSize + 0.01)
            {
                timer.closeTelon();
                AudioManager.instance.Play(AudioManager.ESounds.telon);
            }
        }
        
    }
}
