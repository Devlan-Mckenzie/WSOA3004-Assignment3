using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SocialPlatforms;

public class VignetteController : MonoBehaviour
{    
    private Volume V;
    private Vignette VG;    
    [Range(0f, 1f)]
    public float LerpInterpolation;
    public float LerpSpeed;

    public float MinVignetteIntensity = 0;
    public float MaxVignetteIntensity = 1;

    private bool isFadingIn = false;
    private bool isFadingOut = false;

    // Start is called before the first frame update
    void Start()
    {
        V = this.gameObject.GetComponent<Volume>();
        V.profile.TryGet(out VG);
       
    }

    // Update is called once per frame
    void Update()
    {
        if (isFadingIn)
        {
            float NewVignetteIntensity = Mathf.Lerp(MinVignetteIntensity, MaxVignetteIntensity, LerpInterpolation);
            LerpInterpolation += LerpSpeed;
            VG.intensity.value = NewVignetteIntensity;
        }

        if (isFadingOut)
        {

            float NewVignetteIntensity = Mathf.Lerp(MaxVignetteIntensity, MinVignetteIntensity, LerpInterpolation);
            LerpInterpolation += LerpSpeed;
            VG.intensity.value = NewVignetteIntensity;
        }
    }

    public void VignetteFadeIn()
    {        
        isFadingIn = true;
        isFadingOut = false;
        
    }

    public void VignetteFadeOut()
    {
        isFadingOut = true;
        isFadingIn = false;
    }
}
