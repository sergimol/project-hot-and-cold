using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickSound()
    {
        AudioManager.instance.Play(AudioManager.ESounds.click);
    }
    public void OverSound()
    {
        AudioManager.instance.changePitch(AudioManager.ESounds.pop);
        AudioManager.instance.Play(AudioManager.ESounds.pop);
    }

    public void ExitSound()
    {
        AudioManager.instance.resetPitch(AudioManager.ESounds.pop);
    }
}
