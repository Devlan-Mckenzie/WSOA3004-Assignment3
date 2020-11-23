using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEffect : MonoBehaviour
{
    private bool PlayEffect = false;
    private float Timer = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayEffect)
        {
            this.GetComponent<SpriteRenderer>().enabled = true;
            Timer -= Time.deltaTime;
            if (Timer < 0)
            {
                Timer = 2f;
                PlayEffect = false;
                this.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

    public void PlayHurtEffect()
    {
        PlayEffect = true;
    }
}
