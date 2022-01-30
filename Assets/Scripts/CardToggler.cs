using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardToggler : MonoBehaviour
{

    int pos;
    bool facil, custom;

    public void setValues(int p, bool a, bool b)
    {
        pos = p; facil = a; custom = b;
    }
    public void Toggle(bool active)
    {
        Baraja.instance.DesativarCarta(active, pos, facil, custom);
        AudioManager.instance.Play(AudioManager.ESounds.botonClick);
    }
}
