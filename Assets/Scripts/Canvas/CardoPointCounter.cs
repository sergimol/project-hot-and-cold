using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardoPointCounter : MonoBehaviour
{
    Transform point0, textJug;
    [SerializeField]
    Text text1, puntos1;


    float origCard1, origText;

    [SerializeField]
    float lerpTime;

    float startTime = 2;
    // Start is called before the first frame update
    void Start()
    {
        int points = GameManager.instance.actualCardPoints;
        GameManager.instance.actualCardPoints = 0;
        point0 = gameObject.transform.GetChild(5-points);
        textJug = gameObject.transform.GetChild(6);

        //text1 = point0.gameObject.GetComponentInChildren<Text>();
        //puntos1 = point0.gameObject.GetComponentsInChildren<Text>()[1];

        //text1.text = Baraja.instance.GiveSeleccion().descripcion;
        //puntos1.text = Baraja.instance.GiveSeleccion().puntos.ToString();

        origCard1 = point0.position.y;
        origText = textJug.position.y;
    }

    void Update()
    {
        if (startTime > 1)
        {
            startTime -= Time.deltaTime;
        }
        else if (startTime > 0)
        {
            startTime -= Time.deltaTime;
            point0.position = Vector3.Lerp(point0.position, new Vector3(point0.position.x, origCard1 + 3.35f * gameObject.GetComponent<RectTransform>().rect.height / 4, point0.position.z), lerpTime);
        }
        else
        {
            textJug.position = Vector3.Lerp(textJug.position, new Vector3(textJug.position.x, origText - gameObject.GetComponent<RectTransform>().rect.height / 4, textJug.position.z), lerpTime);
        }
    }
}
