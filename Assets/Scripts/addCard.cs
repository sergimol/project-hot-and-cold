﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class addCard : MonoBehaviour
{
    int points = 0;
    [SerializeField]
    Text textbox;

    [SerializeField]
    Animator textInfoAnim;


    public bool esfacil = false;


    public void dificulty(int a)
    {
        points = a;
    }
    public void toggleFacil(bool a)
    {
        esfacil = a;
    }
    public void novaCarta()
    {
        if (points == 0 || textbox.text == "") return;
        Baraja.instance.addCartaCustom(esfacil, points, textbox.text);
        textInfoAnim.SetTrigger("info");
    }
}
