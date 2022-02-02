using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class MainMenu : MonoBehaviour
{
    [SerializeField]
    GameObject optionsFirstButton = null, optionsUI = null, modeFirstButton = null, modeUI = null, mainUI = null, deckPanel = null, content = null; // Referencian el menú de opciones y el botón seleccionado al abrirlo

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
        //EventSystem.current.SetSelectedGameObject(null);
        //EventSystem.current.SetSelectedGameObject(modeFirstButton);
        mainUI.SetActive(false);
        AudioManager.instance.Play(AudioManager.ESounds.botonClick);
    }

    public void Quit() // Cierra el juego
    {
        AudioManager.instance.Play(AudioManager.ESounds.botonClick);
        Application.Quit();
    }

    public void Credits()
    {
        AudioManager.instance.Play(AudioManager.ESounds.botonClick);
        GameManager.instance.goToCreditsScene();
    }

    public void Options() // Abre el menú de opciones
    {
        AudioManager.instance.Play(AudioManager.ESounds.botonClick);
        optionsUI.SetActive(true);
        //EventSystem.current.SetSelectedGameObject(null);
        //EventSystem.current.SetSelectedGameObject(optionsFirstButton);
        mainUI.SetActive(false);
        //Debug.Log("Aqui van las opciones");
    }

    public void Deck()
    {
        AudioManager.instance.Play(AudioManager.ESounds.botonClick);

        deckPanel.SetActive(true);
        //EventSystem.current.SetSelectedGameObject(null);
        //EventSystem.current.SetSelectedGameObject(content.GetComponentInChildren<Transform>().gameObject); //todo
        mainUI.SetActive(false);


        Debug.Log("Aqui van las cartas");
    }
}
