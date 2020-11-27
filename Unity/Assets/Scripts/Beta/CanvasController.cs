using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public Text Prisoner2Text;

    
    public void CanvasFlip()
    {
        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

        TextFlip();
    }

    public void TextFlip()
    {
        Vector3 theScale = Prisoner2Text.transform.localScale;
        theScale.x *= -1;

        Prisoner2Text.transform.localScale = theScale;
    }
}
