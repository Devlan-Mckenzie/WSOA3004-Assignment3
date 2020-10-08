﻿using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioClip audioClip;

    [Range(0f, 5f)]
    public float volume;

    [Range(0f, 2f)]
    public float pitch;

    public string name;
    public bool loop;

    [HideInInspector]
    public AudioSource audioSource;

}
