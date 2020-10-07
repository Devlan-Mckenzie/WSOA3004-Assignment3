using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    
    public void Closegame()
    {
        Application.Quit();
    }

    public void resume()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }

    public void rules()
    {
        SceneManager.LoadScene(2);
    }

    public void next()
    {
        SceneManager.LoadScene(3);
    }


    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
