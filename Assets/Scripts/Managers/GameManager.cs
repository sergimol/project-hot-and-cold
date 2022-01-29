using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] float startTime;
    [SerializeField] int rounds;
    public static GameManager instance;
    private int score = 0;
    private int actualCardPoints = 0;
    public int actualObject = 0;
    public float mainVolSlider = 0.2f,
                 SFXVolSlider = 0.2f,
                 musicVolSlider = 0.2f;
    public bool fullScreenToggle = true,
               gameIsPaused;

    private int[] sceneNumbers = new int[2] { 1, 2 };
    private int actualScene;
    private int possibleNum;
    private System.Random rnd = new System.Random();
    private string nextSceneName;
    // En el método Awake comprueba si hay otro GameManger
    // y si no lo hay se inicializa como GameManager. En el caso
    // que hubiera otro se autodestruye
    void Awake()
    {
        possibleNum = sceneNumbers.Length;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        nextScene();
        nextSceneName = "CardScene";
    }

    public void ChangeScene()
    {
        string aux = nextSceneName;
        switch (nextSceneName)
        {
            case "HideObjectScene":
                nextSceneName = "CardScene";
                break;
            case "CardScene":
                Cursor.visible = true;
                nextSceneName = "Level";
                break;
            case "Level":
                nextSceneName = "ScoreScene";
                break;
            case "ScoreScene":
                if (rounds > 0)
                {
                    nextScene();
                    nextSceneName = "HideObjectScene";
                }
                else
                    nextSceneName = "FinalScene";
                break;
        }
        if (aux == "HideObjectScene" || aux == "Level")
            aux += actualScene;
        //startTime = 30;
        SceneManager.LoadScene(aux);
    }

    public void addPoints()
    {
        score += actualCardPoints;
    }

    public void FullscreenToggleState(bool isFullscreen)
    {
        if (isFullscreen)
            fullScreenToggle = true;
        else
            fullScreenToggle = false;
    }

    public void MainSliderState(float volume)
    {
        mainVolSlider = volume;
    }

    public void MusicSliderState(float volume)
    {
        musicVolSlider = volume;
    }
    public void SFXSliderState(float volume)
    {
        SFXVolSlider = volume;
    }

    private void nextScene()
    {
        int x = rnd.Next(possibleNum);
        actualScene = sceneNumbers[x];
        sceneNumbers[x] = sceneNumbers[possibleNum - 1];
        sceneNumbers[possibleNum - 1] = actualScene;
        possibleNum--;
        rounds--;
    }

    public int getId() { return actualScene; }
    public string getName() { return nextSceneName; }
}
