using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField]
    AudioMixer master = null; // Utilizamos el mixer para controlar el volumen

    [SerializeField]
    GameObject pauseMenuUI = null, optionsMenuUI = null, pauseFirstButton = null, mainFirstButton = null, mainUI = null; // Referencian los demás menus y que botón debería estar seleccionado al volver a ellos

    [SerializeField]
    GameObject mainVolSlider, SFXVolSlider, musicVolSlider, fulscreenToggle;

    private void Start()
    {
        mainVolSlider.GetComponent<Slider>().value = GameManager.instance.mainVolSlider;
        SFXVolSlider.GetComponent<Slider>().value = GameManager.instance.SFXVolSlider;
        musicVolSlider.GetComponent<Slider>().value = GameManager.instance.musicVolSlider;
        fulscreenToggle.GetComponent<Toggle>().isOn = Screen.fullScreen;
        //deathToggle.GetComponent<Toggle>().isOn = !GameManager.instance.toggleDeath;


    }

    public void SetMasterVolume(float volume) // Slider del volumen general
    {
        master.SetFloat("masterVolume", Mathf.Log10(volume) * 20);
        GameManager.instance.MainSliderState(volume);
        AudioManager.instance.Play(AudioManager.ESounds.sliderMaster);
    }

    public void SetMusicVolume(float volume) // Slider del volumen de la música
    {
        master.SetFloat("musicVolume", Mathf.Log10(volume) * 20);
        GameManager.instance.MusicSliderState(volume);
        AudioManager.instance.Play(AudioManager.ESounds.sliderMusic);
    }

    public void SetSFXVolume(float volume) // Slider del volumen de los efectos
    {
        master.SetFloat("sfxVolume", Mathf.Log10(volume) * 20);
        GameManager.instance.SFXSliderState(volume);
        AudioManager.instance.Play(AudioManager.ESounds.slider);
    }

    public void SetFullScreen(bool isFullscreen) // Controla si el juego debe estar a pantalla completa o no
    {
        Screen.fullScreen = isFullscreen;
        GameManager.instance.FullscreenToggleState(isFullscreen);
        AudioManager.instance.Play(AudioManager.ESounds.botonClick);
    }

    public void Back() // Es llamado al salir del menú de opciones
    {
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true);
            //EventSystem.current.SetSelectedGameObject(null);
            //EventSystem.current.SetSelectedGameObject(pauseFirstButton);
        }
        else if (mainFirstButton != null)
        {
            mainUI.SetActive(true);
            //EventSystem.current.SetSelectedGameObject(mainFirstButton);
        }
        optionsMenuUI.SetActive(false);
        AudioManager.instance.Play(AudioManager.ESounds.botonClick);
    }

    /*public void DeathToggle(bool death) // God mode only
    {
        GameManager.instance.DeathToggle(death);
    }*/
}
