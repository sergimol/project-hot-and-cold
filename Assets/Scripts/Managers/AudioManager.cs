using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    Sound[] sounds = null; // Array que contiene todos los sonidos del juego

    public static AudioManager instance;
    [SerializeField]
    AudioMixerGroup sfx;

    public enum ESounds { acierto, botonClick, botonInicio, fallo, slider, sliderMusic, sliderMaster, footsteps, anuncioEscondite, telon, temon,
                          click, pop}; // Enum usado para acceder al array sounds

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else        
            Destroy(this.gameObject);
        

        foreach (Sound s in sounds) // Asigna a cada sonido un AudioSource con las características correspondientes
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip; 
            s.source.outputAudioMixerGroup = s.mixer; 
            s.source.loop = s.loop;
            s.source.volume = s.volume;
            s.source.playOnAwake = false;
            s.source.pitch = 1;
        }
    }

    public void Play (ESounds sound) // Hace sonar el sonido que corresponda
    {
        int i = (int)sound;
        Sound s = sounds[i];
        if(!s.source.isPlaying)
            s.source.Play();
    }

    public void Stop (ESounds sound) // Para el sonido que corresponda
    {
        int i = (int)sound;
        Sound s = sounds[i];
        if(s.source.isPlaying)
            s.source.Stop();
    }

    public void StopAll() // Para todos los sonidos
    {
        Sound s;
        for (int i = 0; i < sounds.Length; i++)
        {
            s = sounds[i];
            if (s.source.isPlaying)
                s.source.Stop();
        }
    }

    public void StopAllSFX() // Para todos los efectos de sonido
    {
        Sound s;
        for(int i = 0; i < sounds.Length; i++)
        {
            s = sounds[i];
            if (s.source.outputAudioMixerGroup == sfx) 
                s.source.Stop();
        }
    }

    public bool IsPlaying(ESounds sound) // Comprueba si un clip está sonando o no
    {
        int i = (int)sound;
        Sound s = sounds[i];
        if (s.source.isPlaying)
            return true;
        else
            return false;
    }

    public void increasePitch(ESounds sound)
    {
        int i = (int)sound;
        Sound s = sounds[i];
        float actualPitch = s.source.pitch;
        if (actualPitch < 2)
            s.source.pitch = actualPitch + 0.05f;
    }
    public void changePitch(ESounds sound)
    {
        int i = (int)sound;
        Sound s = sounds[i];
        float actualPitch = s.source.pitch;    
        int r = Random.Range(-2, 1);
        float offset;
        if (r == -1)
            offset = -0.1f;
        else if (r == 0)
            offset = 0;
        else
            offset = 0.1f;
        s.source.pitch = actualPitch + offset;
    }
    public void resetPitch(ESounds sound)
    {
        int i = (int)sound;
        Sound s = sounds[i];
        s.source.pitch = 1f;
    }
}


