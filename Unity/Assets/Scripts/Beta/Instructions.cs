using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Instructions : MonoBehaviour
{
    public GameObject SpeechBackground;
    public Text instructions;
    [Header("5 Rooms and instructions for each")]
    public string[] Instructionstext;

    private int RoomCounter;
    // Start is called before the first frame update
    void Start()
    {
        SpeechBackground = this.gameObject;
        RoomCounter = SceneManager.GetActiveScene().buildIndex;
        
    }
    public void ShowInstructions()
    {
        if (!SpeechBackground.activeSelf)
        {
            SpeechBackground.SetActive(true);
        }

        if(SpeechBackground.activeSelf)
        {
            instructions.text = Instructionstext[RoomCounter-1];
            
        }
        
    }

    
    // Update is called once per frame
    void Update()
    {
        ShowInstructions();
    }
}
