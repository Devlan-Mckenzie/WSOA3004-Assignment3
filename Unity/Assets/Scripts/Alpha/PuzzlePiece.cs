using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{

    public GameObject Zone;                             // Stores the zone the puzzle is in
    public bool Puzzle = false;                         // Boolean which controls whether this is a key puzzle solving piece or not
    // Start is called before the first frame update
    void Start()
    {
        // Check if the zone has been set and this is a key puzzle piece, if not set say so
        if (Zone == null && Puzzle)
        {
            Debug.Log("Puzzle Zone not set on Puzzle Piece");
        }
    }    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If this is a puzzle piece 
        if (Puzzle)
        {
            // Check to see if placed in the correct puzzle complete trigger zone
            if (collision.gameObject.tag == "PuzzleComplete")
            {
                // Tell the zone that the puzzle has been solved
                Zone.GetComponent<PuzzleTimer>().PuzzleComplete();
            }
        }
       
    }
}
