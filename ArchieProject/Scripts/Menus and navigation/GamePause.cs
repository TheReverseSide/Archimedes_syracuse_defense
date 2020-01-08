using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePause : MonoBehaviour
{

    public static bool gameIsPaused;
    public GameObject pauseText;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        Debug.Log("toggling.." + gameIsPaused);


        if (gameIsPaused)
        {
            Resume();
        }
        else
        {

            Pause();
        }
    }

    void Resume()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        pauseText.SetActive(false);
    }

    void Pause()
    {
        Time.timeScale = 0f;
        gameIsPaused = true;
        pauseText.SetActive(true);
    }

    void FastForward()
    {
        Time.timeScale = 2f;
    }



}
