using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CardTimer : MonoBehaviour
{
    [SerializeField]
    Telon telon;

    public void closeTelon()
    {
        telon.ini = false;
        telon.reposition();
    }
}
