﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class MainMenu : MonoBehaviour
{
    [SerializeField]
    GameObject optionsFirstButton = null, optionsUI = null, modeFirstButton = null, modeUI = null, mainUI = null; // Referencian el menú de opciones y el botón seleccionado al abrirlo

    private void Start() // Inicia la música del menú
    {
        //AudioManager.instance.Play(AudioManager.ESounds.Menu);
    }

    public void Play() // Para la música del menú y carga la primera escena
    {
        //Esto está ahora en ModeMenu
        //Debug.Log("HideObjectScene" + GameManager.instance.getId());
        //SceneManager.LoadScene("HideObjectScene"+GameManager.instance.getId());
        modeUI.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(modeFirstButton);
        mainUI.SetActive(false);
    }

    public void Quit() // Cierra el juego
    {
        Application.Quit();
    }

    public void Credits()
    {
        //SceneManager.LoadScene("Creditos");
        Debug.Log("Aqui van los creditos");
    }

    public void Options() // Abre el menú de opciones
    {
        optionsUI.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
        mainUI.SetActive(false);
        //Debug.Log("Aqui van las opciones");
    }

    public void Deck()
    {
        Debug.Log("Aqui van las cartas");
    }
}
