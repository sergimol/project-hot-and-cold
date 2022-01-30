using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSceneFina : MonoBehaviour
{
    Transform points, textJug;

    float origCard1, origText;

    [SerializeField]
    float lerpTime;

    float startTime = 2;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        int pointsFin = GameManager.instance.score;
        GameManager.instance.score = 0;
        points = gameObject.transform.GetChild(3);
        textJug = gameObject.transform.GetChild(2);

        points.GetComponent<Text>().text = pointsFin.ToString();

        origCard1 = points.position.y;
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
            textJug.position = Vector3.Lerp(textJug.position, new Vector3(textJug.position.x, origText - gameObject.GetComponent<RectTransform>().rect.height / 2, textJug.position.z), lerpTime);
        }
        else
        {
            points.position = Vector3.Lerp(points.position, new Vector3(points.position.x, origCard1 + 2.8f * gameObject.GetComponent<RectTransform>().rect.height / 4, points.position.z), lerpTime);
        }
    }
}
