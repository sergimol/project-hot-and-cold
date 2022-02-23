using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class setToogle : MonoBehaviour
{
    // Start is called before the first frame update
    Toggle thisToogle;
    
    void Start()
    {
        thisToogle = GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {
        //Esto es basura literalmente espero que no lo vea nadie jamas
        if (thisToogle.isOn)
            thisToogle.image.sprite = thisToogle.spriteState.selectedSprite;
        else
            thisToogle.image.sprite = thisToogle.spriteState.disabledSprite;

    }
}
