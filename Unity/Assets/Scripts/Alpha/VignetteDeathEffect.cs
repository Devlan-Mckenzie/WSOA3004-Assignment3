using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SocialPlatforms;

public class VignetteDeathEffect : MonoBehaviour
{
    private GameObject Player;
    private Volume V;
    private Vignette VG;
    [Header("Offsets the lerp")]
    public float VignetteOffset = 0.5f;
    [Range(0f,1f)]
    public float LerpSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.GetComponent<CharacterController>().PlayerisDead())
        {
            V = this.gameObject.GetComponent<Volume>();
            V.profile.TryGet(out VG);
            float LerpTime = Player.GetComponent<CharacterController>().DeathAnimLength;
            float VignetteTime =+ Time.deltaTime;
            float CurrentVignetteIntensity = VG.intensity.value;         
            int MaxVignetteIntensity = 1;
            float NewVignetteIntensity = Mathf.Lerp(CurrentVignetteIntensity, MaxVignetteIntensity, ((VignetteTime/LerpTime) * LerpSpeed) + VignetteOffset);
            VG.intensity.value = NewVignetteIntensity;
        }
    }
}
