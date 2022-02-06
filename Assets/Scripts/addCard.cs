using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class addCard : MonoBehaviour
{
    int points = 1;
    [SerializeField]
    Text textbox;

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
        Baraja.instance.addCartaCustom(esfacil, points, textbox.text);
    }

}
