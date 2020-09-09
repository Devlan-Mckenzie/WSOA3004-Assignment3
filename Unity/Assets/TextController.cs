using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextController : MonoBehaviour
{
    public Text FriendsSpeech;
    public GameObject Player;

    public int FriendCount=0;
    public float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        Player = this.gameObject;
    }

    void changetext()
    {
        if(FriendCount == 1)
        {
            FriendsSpeech.text = "hello 1";
        }

        else if (FriendCount == 2)
        {
            FriendsSpeech.text = "hello 2";
        }

        else if (FriendCount == 3)
        {
            FriendsSpeech.text = "hello 3";
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Friend")
        {
            if(time >= 5)
            {
                FriendCount++;
                time = 0;
            }
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        time += Time.deltaTime;
        changetext();
    }
}
