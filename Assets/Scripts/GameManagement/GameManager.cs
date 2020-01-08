using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public static bool GameIsOver;

    public AudioSource archie_intro_fleet;
    public AudioSource archie_gameplay;
    public AudioSource archie_victory;
    public AudioSource archie_defeat;

    //public AudioManager audioManager;

    public GameObject gameOverUI;
    public GameObject completeLevelUI;
    //public GameObject 

    bool gameplayMusicStarted;

    void Start()
    {
        GameIsOver = false;
        archie_intro_fleet.Play();
    }

    void Update()
    {
        if (GameIsOver)
            return;

        if (!archie_intro_fleet.isPlaying && !gameplayMusicStarted)
        { //Intro fleet music stopped, start the normal stuff
            gameplayMusicStarted = true;
            archie_gameplay.Play();
            archie_gameplay.loop = true;
        }

        if (PlayerStats.health <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        GameIsOver = true;
        archie_gameplay.Stop();
        archie_defeat.Play();
        gameOverUI.SetActive(true);
    }

    public void WinLevel()
    {
        GameIsOver = true;
        archie_gameplay.Stop();
        archie_victory.Play();
        completeLevelUI.SetActive(true);
    }
}
