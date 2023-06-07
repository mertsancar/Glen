using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public GameObject pauseScreen;


    public void Update()
    {
        PauseGame();
    }
    public void NewScene()
    {
        SceneManager.LoadScene("Game");
    }

    public void PauseGame() 
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseScreen.activeSelf == false)
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && pauseScreen.activeSelf == true)
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void Resume()
    {
        
    }

    public void ReturnMain() 
    {
        SceneManager.LoadScene("Menu");

    }

    public void Quit()
    {
        //Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

}
