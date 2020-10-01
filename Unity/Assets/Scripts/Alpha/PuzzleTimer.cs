using UnityEngine;
using TMPro;

public class PuzzleTimer : MonoBehaviour
{
    public UnityEngine.UI.Slider TimerSlider;               // Stores the puzzle timer slider                        
    public TextMeshProUGUI  TimerText;                      // Stores a reference to the text part of the timer 
    public float TimeGiven = 10;                            // Time Given to the player to complete the puzzle

    private float TimerCount = 0;                           // Current time passed from start of puzzle
    [SerializeField] bool StartTimer = false;               // Controls the starting of the puzzle
    [SerializeField] bool PuzzleCompleted = false;          // Controls the puzzle completion 
    float SliderTime;                                       // Stores the Slider time which has been converted into min and sec

    private GameObject Player;                              // Stores the plaeyr gameobject

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        // set the timer to off
        StartTimer = false;
        // if the timer max and timer value are wrong set them to the correct max and value
        if (TimerSlider.maxValue != TimeGiven)
        {            
            TimerSlider.maxValue = TimeGiven;           
        }
        if (TimerSlider.value != TimeGiven)
        {            
            TimerSlider.value = TimeGiven;            
        }
        // set the slider time to be equal to the time provided
        SliderTime = TimeGiven;
    } 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // on trigger enter check if it was the player 
        if (collision.gameObject.CompareTag("Player"))
        {
            // if it was the player start the timer
            StartTimer = true;
            TimerSlider.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // On trigger exit check if it was the player 
        if (collision.gameObject.CompareTag("Player"))
        {
            // if it was the player then set the timer to stop
            StartTimer = false;
        }
    }

    private void Update()
    {
        // if the timer is running 
        if (StartTimer)
        {
            // display the timer 
            TimerSlider.gameObject.SetActive(true);
            // start subtracting time from the total time given
            SliderTime -= Time.deltaTime;
            // start timer for Timecount
            TimerCount += Time.deltaTime;
            // convert that time into min and sec
            float SliderMinutes = Mathf.FloorToInt(SliderTime / 60);
            float SliderSeconds =  Mathf.FloorToInt(SliderTime - SliderMinutes *60f);    
            // store that time in a variable called textTime which is in the format of minutes:seconds
            string textTime = string.Format("{0:0}:{1:00}", SliderMinutes, SliderSeconds);
            //if the timer runs out
            if (SliderTime <= 0)
            {
                // stop the timer 
                StartTimer = false;                
            }          
            // update the slider text and value with all the new values until timer runs out
            TimerText.text = textTime;
            TimerSlider.value = SliderTime;            
        }
        else
        {
            
        }

        // if puzzle is not complete
        if (!PuzzleCompleted)
        {
            // if time passed is greater than time given
            if (TimerCount > TimeGiven)
            {
                TimerText.text = "00:00";
                // find the audio manager and play death sound
                FindObjectOfType<AudioManager>().Play("PlayerDeath"); // Ben Insert death sound here thanks :)         

                // create player death of some kind 
                // for now sets the anim bool death to true;
                Player.GetComponent<CharacterController>().PlayerDeath();
            }
        }
        else
        {
            // if puzzle gets completed the make the timer inactive
            TimerSlider.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
        
    }

    public void PuzzleComplete()
    {
        PuzzleCompleted = true;
    }

   


}
