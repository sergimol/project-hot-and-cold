using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CardDealer : MonoBehaviour
{
    Transform card1, card2;
    Text text1, text2, puntos1, puntos2;

    [SerializeField]
    Font textFont;

    [SerializeField]
    Color textColor;
    //Sprite[] redIm = new Sprite[3];
    //[SerializeField]
    //Sprite[] blueIm = new Sprite[3];

    float origCard1, origCard2;

    [SerializeField]
    float lerpTime;


    bool endAnimation = false;

    float startTime = 2;
    // Start is called before the first frame update
    void Start()
    {
        endAnimation = false;

        card1 = gameObject.transform.GetChild(2);
        card2 = gameObject.transform.GetChild(3);

        text1 = card1.gameObject.GetComponentInChildren<Text>();
        text2 = card2.gameObject.GetComponentInChildren<Text>();
        puntos1 = card1.gameObject.GetComponentsInChildren<Text>()[1];
        puntos2 = card2.gameObject.GetComponentsInChildren<Text>()[1];

        text1.text = Baraja.instance.GiveCartaFacil().descripcion;
        text2.text = Baraja.instance.GiveCartaDificil().descripcion;

        text1.font = textFont;
        text2.font = textFont;

        text1.color = textColor;
        text2.color = textColor;

        puntos1.text = Baraja.instance.GiveCartaFacil().puntos.ToString();
        puntos2.text = Baraja.instance.GiveCartaDificil().puntos.ToString();

        origCard1 = card1.position.y;
        origCard2 = card2.position.y;

        //card1.GetComponent<Image>().sprite = redIm[Int32.Parse(puntos1.text) - 1];
        //card2.GetComponent<Image>().sprite = blueIm[Int32.Parse(puntos2.text) - 1];

        //Debug.Log(card1.GetComponent<Image>().sprite);
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
        else if (startTime > 0.7f)
        {
            startTime -= Time.deltaTime;
            card1.position = Vector3.Lerp(card1.position, new Vector3(card1.position.x, origCard1 + 3.2f * gameObject.GetComponent<RectTransform>().sizeDelta.y / 4, card1.position.z), lerpTime);

        }
        else if (!endAnimation)
        {
            card1.position = Vector3.Lerp(card1.position, new Vector3(card1.position.x, origCard1 + 3.2f * gameObject.GetComponent<RectTransform>().sizeDelta.y / 4, card1.position.z), lerpTime);
            card2.position = Vector3.Lerp(card2.position, new Vector3(card2.position.x, origCard2 + 3.2f * gameObject.GetComponent<RectTransform>().sizeDelta.y / 4, card2.position.z), lerpTime);

            if (card2.position.y >= origCard2 + 3.2f * gameObject.GetComponent<RectTransform>().sizeDelta.y / 4 - 0.1)
            {
                card1.GetComponent<Animator>().SetInteger("Points", Int32.Parse(puntos1.text));
                card2.GetComponent<Animator>().SetInteger("Points", Int32.Parse(puntos2.text));

                card1.GetComponent<Button>().interactable = true;
                card2.GetComponent<Button>().interactable = true;

                card1.GetComponent<Shake>().enabled = true;
                card2.GetComponent<Shake>().enabled = true;

                endAnimation = true;
            }
        }

    }

    public void setSeleccionBaraja(bool esfacil)
    {
        Baraja.instance.setSeleccion(esfacil);
    }

}
