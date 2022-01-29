using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardoPointCounter : MonoBehaviour
{
    [SerializeField]
    Transform card1;
    [SerializeField]
    Text text1, puntos1;


    float origCard1, origCard2;

    [SerializeField]
    float lerpTime;

    float startTime = 2;
    // Start is called before the first frame update
    void Start()
    {
        card1 = gameObject.transform.GetChild(4);

        text1 = card1.gameObject.GetComponentInChildren<Text>();
        puntos1 = card1.gameObject.GetComponentsInChildren<Text>()[1];

        text1.text = Baraja.instance.GiveSeleccion().descripcion;
        puntos1.text = Baraja.instance.GiveSeleccion().puntos.ToString();

        origCard1 = card1.position.y;
    }

    void Update()
    {
        if (startTime > 1)
        {
            startTime -= Time.deltaTime;
        }
        else
        {
            startTime -= Time.deltaTime;
            card1.position = Vector3.Lerp(card1.position, new Vector3(card1.position.x, origCard1 + 750, card1.position.z), lerpTime);
        }
    }
}
