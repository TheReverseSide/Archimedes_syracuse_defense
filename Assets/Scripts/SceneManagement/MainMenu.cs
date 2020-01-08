using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string levelToLoad = "Level01Intro";

    public void Quit()
    {
        Debug.Log("Exiting...");
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level01Intro");
    }

    public void StartPlaying()
    {
        SceneManager.LoadScene("Level01");
    }

    //public void GoToCredits()
    //{
    //    SceneSwapper.swapper.JumpToCredits();
    //    Debug.Log("Credits");
    //}

}
