using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField] Text timerText;
    [SerializeField] float startTime;
    // Start is called before the first frame update
    void Start()
    {
        //startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        startTime -= Time.deltaTime;

        string seconds = Math.Truncate((startTime % 60)).ToString();

        timerText.text = seconds;
    }
}
