using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CardDealer : MonoBehaviour
{
    [SerializeField]
    Transform card1, card2;

    float origCard1, origCard2;

    [SerializeField]
    float lerpTime;

    float startTime = 2;
    // Start is called before the first frame update
    void Start()
    {
        card1 = gameObject.transform.GetChild(2);
        card2 = gameObject.transform.GetChild(3);

        origCard1 = card1.position.y;
        origCard2 = card2.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (startTime > 1)
        {
            //Si ha perdido
            startTime -= Time.deltaTime;

            string seconds = Math.Truncate((startTime % 60)).ToString();
        }
        else if(startTime > 0.7f)
        {
            startTime -= Time.deltaTime;
            card1.position = Vector3.Lerp(card1.position, new Vector3(card1.position.x, origCard1 + 750, card1.position.z), lerpTime);
            
        }
        else
        {
            card1.position = Vector3.Lerp(card1.position, new Vector3(card1.position.x, origCard1 + 750, card1.position.z), lerpTime);
            card2.position = Vector3.Lerp(card2.position, new Vector3(card2.position.x, origCard2 + 750, card2.position.z), lerpTime);
        }

    }
}
