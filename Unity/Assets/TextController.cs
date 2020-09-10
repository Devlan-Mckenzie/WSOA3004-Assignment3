using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextController : MonoBehaviour
{
    public Text FriendsSpeech;
    public GameObject Player;
    public GameObject uitext;

    public int FriendCount=0;
    public float time = 0;
    public bool UIDisplay = false;
    // Start is called before the first frame update
    void Start()
    {
        Player = this.gameObject;
    }

    void changetext()
    {
        if(FriendCount == 1)
        {
            FriendsSpeech.text = "Thank you for rescuing me , here is a key you will need to exit this dungeon. Find the other 2 and get their keys. I will meet you at the end ! Good Luck";
        }

        else if (FriendCount == 2)
        {
            FriendsSpeech.text = "Thank God , you found me ! Here is the second Key , find our last friend for the last key!";
        }

        else if (FriendCount == 3)
        {
            FriendsSpeech.text = "Finally! I have been waiting for you , heres the last key , hurry and get to the final door so we can escape this hell hole!";
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
                UIDisplay = true;
            }
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        time += Time.deltaTime;
        changetext();
        if (time >= 10 && UIDisplay)
        {
            UIDisplay = false;
        }

        uitext.SetActive(UIDisplay);
    }
}
