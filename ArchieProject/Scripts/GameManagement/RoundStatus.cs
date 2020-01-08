using UnityEngine;
using UnityEngine.UI;

public class RoundStatus : MonoBehaviour
{

    public Text roundStatus;
    public WaveSpawner waveSpawner;


    void Update() //should I make a coroutine to make a delay first?
    {
        if (waveSpawner.isSpawning) roundStatus.text = "ENEMIES SPAWNING...";
        else if (waveSpawner.EnemiesInGame() > 0) roundStatus.text = "WAVE IN PROGRESS...";
        else roundStatus.text = "PLEASE WAIT...";
    }
}
