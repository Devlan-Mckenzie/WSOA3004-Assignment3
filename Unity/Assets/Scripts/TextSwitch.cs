using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSwitch : MonoBehaviour
{
    public GameObject uitext;
    public bool displaytext = false;
    public float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    
        
    
    {
        if (collision.gameObject.tag == "Player")
        {
            displaytext = true;
        }
    }

    public void TextDisplay()
    {

        uitext.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        

        if (displaytext)
        {
            timer += Time.deltaTime;
            TextDisplay();
        }

        if (displaytext && timer >= 5)
        {
            displaytext = false;
            uitext.SetActive(false);
            timer = 0;
        }
    }
}
