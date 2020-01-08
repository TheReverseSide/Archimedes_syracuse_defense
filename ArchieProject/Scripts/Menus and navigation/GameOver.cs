using UnityEngine;
using UnityEngine.SceneManagement;

public class
GameOver : MonoBehaviour //In charge of retrying and going to the menu
{
    public string menuSceneName = "MainMenu";

    public Canvas overlayCanvas;

    public void OnEnable()
    {
        //overlayCanvas.enabled = !(overlayCanvas.enabled);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
