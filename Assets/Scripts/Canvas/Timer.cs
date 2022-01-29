using UnityEngine;
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

    private bool easyMode;
    // Start is called before the first frame update
    void Start()
    {
        //startTime = Time.time;
        easyMode = GameManager.instance.easyMode;
    }

    // Update is called once per frame
    void Update()
    {
        if (!easyMode)
        {
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
    }

    void resetTimer(int seconds)
    {
        startTime = seconds;
    }

    public void closeTelon()
    {
        startTime = 0;
        Debug.Log("Se cierra");
        if (easyMode && telon.ini)
        {
            telon.ini = false;
            telon.reposition();
        }
    }
}
