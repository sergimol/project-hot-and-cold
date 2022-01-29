using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ModeMenu : MonoBehaviour
{
    [SerializeField]
    GameObject modeMenuUI = null, mainFirstButton = null, mainUI = null; // Referencian los demás menus y que botón debería estar seleccionado al volver a ellos

    public void WithTime()
    {
        GameManager.instance.easyMode = false;
        Debug.Log("HideObjectScene" + GameManager.instance.getId());
        SceneManager.LoadScene("HideObjectScene" + GameManager.instance.getId());
    }

    public void NoTime()
    {
        GameManager.instance.easyMode = true;
        Debug.Log("HideObjectScene" + GameManager.instance.getId());
        SceneManager.LoadScene("HideObjectScene" + GameManager.instance.getId());
    }

    public void HighScores()
    {
        Debug.Log("Aqui van las highscores");
    }

    public void Back() // Es llamado al salir del menú de seleccion de modo
    {
        if (mainFirstButton != null)
        {
            mainUI.SetActive(true);
            EventSystem.current.SetSelectedGameObject(mainFirstButton);
        }
        modeMenuUI.SetActive(false);
    }

    /*public void DeathToggle(bool death) // God mode only
    {
        GameManager.instance.DeathToggle(death);
    }*/
}
