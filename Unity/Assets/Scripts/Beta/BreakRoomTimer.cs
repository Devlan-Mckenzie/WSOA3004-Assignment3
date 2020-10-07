using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BreakRoomTimer : MonoBehaviour
{

    public GameObject LightsOn;
    public GameObject LightsOff;

    public float timer = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void changescene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && LightsOn.activeSelf)
        {
            LightsOn.SetActive(false);
            LightsOff.SetActive(true);
            Invoke("changescene", 1.5f);
        }
    }
}
