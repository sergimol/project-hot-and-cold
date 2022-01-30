using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckPanel : MonoBehaviour
{
    [SerializeField]
    GameObject panel, panelUI, mainUI, mainFirstButton;
    // Start is called before the first frame update
    void Start()
    {
        Baraja.instance.startPanel(panel);
    }

   public void Back()
    {
        mainUI.SetActive(true);
        EventSystem.current.SetSelectedGameObject(mainFirstButton);
        panelUI.SetActive(false);
        AudioManager.instance.Play(AudioManager.ESounds.botonClick);
    }
}
