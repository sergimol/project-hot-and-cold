using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] float startTime;
    private int score = 0;
    private int actualCardPoints = 0;
    public int actualObject = 0;
    public static GameManager instance;
    // En el método Awake comprueba si hay otro GameManger
    // y si no lo hay se inicializa como GameManager. En el caso
    // que hubiera otro se autodestruye
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void ChangeScene()
    {
        startTime = 30;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void addPoints()
    {
        score += actualCardPoints;
    }
}
