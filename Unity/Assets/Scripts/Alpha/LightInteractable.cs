using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightInteractable : MonoBehaviour
{
    private bool StartTimer = false;
    private float TimePassed = 0f;

    public float TimeAllowed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (StartTimer)
        {
            TimePassed += Time.deltaTime;
            if (TimePassed > TimeAllowed)
            {
                StartTimer = false;
            }
        }
        else
        {
            TimePassed = 0f;
            this.gameObject.tag = "Semi";
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        }    
    }

    public void ToggleInteractable()
    {
        this.gameObject.tag = "PuzzlePiece";
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        StartTimer = true;
        
    }
}
