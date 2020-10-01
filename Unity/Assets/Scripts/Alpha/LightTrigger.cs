using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    public GameObject Zone;                 // Stores the zone the puzzle is in
    public GameObject[] Doors;              // Array of gameobjects to be used as a door storage  
    public GameObject[] Ladders;            // Array of gameobjects to be used as a ladder storage 
    public GameObject[] Torches;            // Array of gameobjects to be used as torches, will be displayed when the light zone is activated
    public bool OpenDoors;                  // Boolean for running the puzzle completed function

    public void OpenDoor()  
    {
        // Tell the zone that the puzzle was completed and thus to stop the timer
        if (Zone != null)
        {
            Zone.gameObject.GetComponent<PuzzleTimer>().PuzzleComplete();
        }

        // Set this object to inactive
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;

        if (Torches.Length > 0)
        {
            int n = 0;
            while (n < Torches.Length)
            {
                Torches[n].SetActive(true);
                n++;
            }
        }
        
        // Run through the array of doors and set each of them to inactive, Opening them for now.
        if (Doors.Length >= 1)
        {
            int n = 0;
            while (n< Doors.Length)
            {
                Doors[n].SetActive(false);
                n++;
            }
            
        }
        // Run through the array of doors and set each of them to active, Showing them for now.
        if (Ladders.Length > 0)
        {
            int n = 0;
            while (n< Ladders.Length)
            {
                Ladders[n].SetActive(true);
                n++;
            }
        }
    }

    

    public void Update()
    {
        // Check if the Puzzle has been solved and then call the above function to display the solve
        if (OpenDoors)
        {
            OpenDoor();
        }
    }
}
