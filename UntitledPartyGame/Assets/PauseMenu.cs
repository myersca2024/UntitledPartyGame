using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseUI;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        pauseUI.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Restart()
    {
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1f;
        Cursor.visible = true;
        isPaused = false;
        SceneManager.LoadScene(0);
    }
}
