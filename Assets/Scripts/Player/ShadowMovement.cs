using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowMovement : MonoBehaviour
{
    [SerializeField]
    float max = 0.55f;

    [SerializeField]
    float min = 0.45f;

    [SerializeField]
    float speed = 0.1f;

    bool side = true;
    // Update is called once per frame
    void Update()
    {
        Vector3 newScale = transform.localScale;
        if(side){
            side = transform.localScale.x < max;
            newScale.x += speed*Time.deltaTime;
        }
        else{
            side = transform.localScale.x < min;
            newScale.x -= speed*Time.deltaTime;
        }

        transform.localScale = newScale;
    }
}
