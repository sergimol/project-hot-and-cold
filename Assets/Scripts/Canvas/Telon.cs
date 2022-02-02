using System;
using UnityEngine;

public class Telon : MonoBehaviour
{
    [SerializeField] Transform tLeft, tRight;
    [SerializeField] float lerpTime;
    public bool ini = true;
    private float originalPosLeft, originalPosRight;
    private RectTransform objectRectTransform;
    // Start is called before the first frame update
    void Start()
    {
        originalPosLeft = tLeft.position.x;
        originalPosRight = tRight.position.x;
        objectRectTransform = gameObject.GetComponent<RectTransform>();
        AudioManager.instance.Play(AudioManager.ESounds.telon);
    }

    // Update is called once per frame
    void Update()
    {
        if (!ini)
        {
            tLeft.position = Vector3.Lerp(tLeft.position, new Vector3(originalPosLeft + objectRectTransform.rect.width / 2 + 10, tLeft.position.y, tLeft.position.z), lerpTime);
            tRight.position = Vector3.Lerp(tRight.position, new Vector3(originalPosRight - objectRectTransform.rect.width / 2 - 10, tRight.position.y, tRight.position.z), lerpTime);

            if (tRight.position.x <= originalPosRight - objectRectTransform.rect.width / 2 - 9.9)
            {
                GameManager.instance.ChangeScene();
            }
        }
        else
        {
            tLeft.position = Vector3.Lerp(tLeft.position, new Vector3(originalPosLeft - objectRectTransform.rect.width / 2 - 10, tLeft.position.y, tLeft.position.z), lerpTime);
            tRight.position = Vector3.Lerp(tRight.position, new Vector3(originalPosRight + objectRectTransform.rect.width / 2 + 10, tRight.position.y, tRight.position.z), lerpTime);
        }
    }

    public void reposition()
    {
        originalPosLeft = tLeft.position.x;
        originalPosRight = tRight.position.x;
    }
}
