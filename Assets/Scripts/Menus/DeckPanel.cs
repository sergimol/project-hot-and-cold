using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckPanel : MonoBehaviour
{
    [SerializeField]
    GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        Baraja.instance.startPanel(panel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
