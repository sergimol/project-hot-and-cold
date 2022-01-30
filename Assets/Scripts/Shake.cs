using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{

    RectTransform rectTransform;
    // Start is called before the first frame update

    [SerializeField]
    float minY = -1.0f, maxY = 1.0f;

    [SerializeField]
    float minZ = -1.0f, maxZ = 1.0f;

    [SerializeField]
    float offset = 0.5f;

    float t = 0.0f;
    void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        float y = rectTransform.rotation.y, z = rectTransform.rotation.z;
        rectTransform.rotation = Quaternion.Euler(0, (Mathf.Lerp(minY, maxY, t)), (Mathf.Lerp(minZ, maxZ, t)));

        t += offset * Time.deltaTime;

        if (t > 1.0f)
        {
            float temp = maxY;
            maxY = minY;
            minY = temp;

            temp = maxZ;
            maxZ = minZ;
            minZ = temp;

            t = 0.0f;
        }
    }

    public void makeBigger()
    {
        if (rectTransform != null)
            rectTransform.localScale = new Vector3(2.2f, 2.2f, 1.0f);
    }

    public void makeSmaller()
    {
        if (rectTransform != null)
            rectTransform.localScale = new Vector3(2f, 2f, 1.0f);
    }
}
