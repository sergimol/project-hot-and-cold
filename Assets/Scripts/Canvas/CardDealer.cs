﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CardDealer : MonoBehaviour
{
    [SerializeField]
    Transform card1, card2;
    [SerializeField]
    Text text1, text2, puntos1, puntos2;


    float origCard1, origCard2;

    [SerializeField]
    float lerpTime;

    float startTime = 2;
    // Start is called before the first frame update
    void Start()
    {
        card1 = gameObject.transform.GetChild(2);
        card2 = gameObject.transform.GetChild(3);

        text1 = card1.gameObject.GetComponentInChildren<Text>();
        text2 = card2.gameObject.GetComponentInChildren<Text>();
        puntos1 = card1.gameObject.GetComponentsInChildren<Text>()[1];
        puntos2 = card2.gameObject.GetComponentsInChildren<Text>()[1];

        text1.text = Baraja.instance.GiveCartaFacil().descripcion;
        text2.text = Baraja.instance.GiveCartaDificil().descripcion;
        puntos1.text = Baraja.instance.GiveCartaFacil().puntos.ToString();
        puntos2.text = Baraja.instance.GiveCartaDificil().puntos.ToString();

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
