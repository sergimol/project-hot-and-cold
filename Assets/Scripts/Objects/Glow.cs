using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glow : MonoBehaviour
{
    float sineAngle = 0;

    private void Update()
    {
        Debug.Log("danlles estoy brillando");
        float sizeOffset = Mathf.Sin(sineAngle);
        transform.localScale = new Vector3(1 + sizeOffset, 1 + sizeOffset, 1);
        sineAngle += Time.deltaTime;
    }

    private void OnDisable()
    {
        //uwu
        transform.localScale = new Vector3(1, 1, 1);
    }
}
