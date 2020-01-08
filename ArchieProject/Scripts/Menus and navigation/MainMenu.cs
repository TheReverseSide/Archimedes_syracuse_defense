using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    public Text welcomeText;

    private void Start()
    {
        if (PlayerStats.isGuest)
        {
            welcomeText.text = "Welcome Mysterious Stranger";
        }
        else
        {
            welcomeText.text = "Welcome, " + PlayerStats.Name;
        }
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Quit()
    {
        Debug.Log("Exiting...");
        Application.Quit();
    }
}
