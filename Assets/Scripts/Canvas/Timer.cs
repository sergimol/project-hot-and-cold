﻿using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField]
    Text timerText;

    [SerializeField]
    float startTime;

    [SerializeField]
    Telon telon;

    private float startTimer;
    bool activated = false;
    // Start is called before the first frame update

    Animator anim;

    void Start()
    {
        //startTime = Time.time;
        anim = timerText.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer > 0)
        {
            startTimer -= Time.deltaTime;
            if (!activated && startTimer < 0.5f)
            {
                startTime -= 3;
                activated = true;
            }
        }
        if (startTime > 1)
        {
            //Si ha perdido
            startTime -= Time.deltaTime;

            string seconds = Math.Truncate((startTime % 60)).ToString();

            timerText.text = seconds;
        }
        else if (telon.ini)
        {
            telon.ini = false;
            telon.reposition();
        }
    }

    void resetTimer(int seconds)
    {
        startTime = seconds;
    }

    public void closeTelon()
    {
        startTime = 0;
        Debug.Log("Se cierra");
    }

    public void reduceTime()
    {
        startTimer = 1.0f;
        activated = false;
        anim.SetTrigger("GrowTusMuertos");
    }
}
