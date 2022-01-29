using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glow : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        anim.enabled = true;
    }

    private void OnDisable()
    {
        //uwu
        transform.localScale = new Vector3(1, 1, 1);
        anim.enabled = false;
    }
}
