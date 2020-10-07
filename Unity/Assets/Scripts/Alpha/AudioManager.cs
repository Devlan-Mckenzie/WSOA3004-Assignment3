using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] m_sounds;

    public static AudioManager m_instance;

    private void Awake()
    {
        if(m_instance == null)
        {
            m_instance = this;
            Debug.Log("AudioManager instance assigned");
            Debug.Log(this.name);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Debug.Log("AudioManager instance was found assigning dont destroy");
            Debug.Log(this.name);
            return;
        }
        foreach (Sound currentSound in m_sounds)
        {
            currentSound.audioSource = gameObject.AddComponent<AudioSource>();
            currentSound.audioSource.clip = currentSound.audioClip;

            currentSound.audioSource.volume = currentSound.volume;
            currentSound.audioSource.pitch = currentSound.pitch;
            currentSound.audioSource.loop = currentSound.loop;
           //currentSound.audioSource.PlayOneShot(currentSound.audioClip);
        }
    }
    public void Start()
    {
        Play("Theme");
        
    }
    public void Play(string name) 
    {
        Sound s = Array.Find(m_sounds, sound => sound.name == name);

        if(s == null)
        {
            return;
        }

        s.audioSource.PlayOneShot(s.audioClip);  
    }
}
