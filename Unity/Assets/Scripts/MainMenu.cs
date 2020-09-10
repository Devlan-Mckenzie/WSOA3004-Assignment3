using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject WinPanel;
    public GameObject DeathPanel;

    public AudioSource DeathPanelSound;

    private void Start()
    {
        WinPanel.SetActive(false);
        DeathPanel.SetActive(false);

        DeathPanelSound.Pause();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        DeathPanelSound.Pause();
    }
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        DeathPanelSound.Pause();
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
