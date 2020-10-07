using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Instructions : MonoBehaviour
{
    public GameObject SpeechBackground;
    public Text intructions;
    public string Instructionstext;

    public int RoomCounter;
    // Start is called before the first frame update
    void Start()
    {
        SpeechBackground = this.gameObject;
        
    }
    public void ShowInstructions()
    {
        if (!SpeechBackground.activeSelf)
        {
            SpeechBackground.SetActive(true);
        }

        if(RoomCounter == 0 && SpeechBackground.activeSelf)
        {
            Instructionstext = "Instructions for the firstroom";
            
        }

        else if (RoomCounter == 1 && SpeechBackground.activeSelf)
        {
            Instructionstext = "Instructions for the secondroom";

        }

        else if (RoomCounter == 2 && SpeechBackground.activeSelf)
        {
            Instructionstext = "Instructions for the thirdroom";

        }

        else if (RoomCounter == 3 && SpeechBackground.activeSelf)
        {
            Instructionstext = "Instructions for the finalroom";

        }

    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
