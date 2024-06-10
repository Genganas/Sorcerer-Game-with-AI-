using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu; 
    private bool isPaused = false; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0f; 
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f; 
            pauseMenu.SetActive(false);
        }
    }
}
