using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CompleteLevel : MonoBehaviour
{
    public string menuSceneName = "MainMenu";
    public Text levelDefeatedText;
    public GameManager gameManager;
    //public SceneFader sceneFader;

    private void Start()
    {
        //levelDefeatedText.text = SceneManager.GetActiveScene().name.ToUpper() + " DEFEATED";
        gameManager = FindObjectOfType<GameManager>();
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Continue()
    {
        if (gameManager != null)
        {
            gameManager.completeLevelUI.SetActive(false);
        }

        FindObjectOfType<Camera>().GetComponent<CameraZoomAndFade>().PlayAnimation();
        // //takes you to next lobe learning scene
    }
}
