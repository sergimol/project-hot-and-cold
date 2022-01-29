﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class MainMenu : MonoBehaviour
{
    [SerializeField]
    GameObject optionsFirstButton = null, optionsUI = null; // Referencian el menú de opciones y el botón seleccionado al abrirlo

    private void Start() // Inicia la música del menú
    {
        //AudioManager.instance.Play(AudioManager.ESounds.Menu);
    }

    public void Play() // Para la música del menú y carga la primera escena
    {
        Cursor.visible = false;
        SceneManager.LoadScene("Sergio");
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
        //optionsUI.SetActive(true);
        //EventSystem.current.SetSelectedGameObject(null);
        //EventSystem.current.SetSelectedGameObject(optionsFirstButton);
        Debug.Log("Aqui van las opciones");
    }

    public void Deck()
    {
        Debug.Log("Aqui van las cartas");
    }
}
