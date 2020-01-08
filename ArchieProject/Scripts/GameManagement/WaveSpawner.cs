using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class WaveSpawner : MonoBehaviour
{
    public bool coroutineRunning = false;
    public bool isSpawning = false;
    bool reachedLastWave;

    public static int EnemiesAlive = 0;

    public Wave[] waves;
    public Transform spawnPoint;

    public float firstWaveCountdown;
    public float timeBetweenWaves;

    Wave wave;

    public Text waveCountdownText;
    public Text roundStatus;

    [Header("Wave information")]
    public Text enemyReadoutInformation;


    private float waveCountdown;
    private int waveIndex = 0; //index of wave to pass to array, starts at 0
    private int tempIndex;

    public GameManager gameManager;

    //public enum SpawnState { Spawning, Waiting, Counting };
    //private SpawnState state = SpawnState.Counting;
    private float searchCountdown = .5f;

    void OnEnable() { EnemiesAlive = 0; }

    void Start()
    {
        waveCountdown = firstWaveCountdown;
    }

    void Update()
    {

        if ((waveCountdown == timeBetweenWaves || waveCountdown == firstWaveCountdown) && !isSpawning)
        { //If the wave is about to start (is in countdown), but no enemies have currently spawned
            //Didnt seem to fire when the countdown occured
            StartCoroutine("WaveInfoSetUp");
            waveCountdown -= Time.deltaTime; //Subtracting so it doesnt run again
        }
        else if (!isSpawning && !reachedLastWave) //Subtract from time as long as there is another wave to countdown and we are not currently spawning
        {
            waveCountdown -= Time.deltaTime;
            waveCountdown = Mathf.Clamp(waveCountdown, 0f, Mathf.Infinity); //Infinity just to make sure countdown wont be less than zero due to a bug or whatever
                                                                            //^to avoid float numbers, use math function
            roundStatus.text = string.Format("{0:00.00}", waveCountdown);
        }

        if (!isSpawning && waveCountdown <= 0 && !reachedLastWave) //If countdown is up and they arent currently spawning AND if the last wave hasnt been reached
        {
            if (coroutineRunning == false) //if not already spawning
            {
                StartCoroutine(SpawnWave());
                waveCountdown = timeBetweenWaves; //reset timer
            }
            return;
        }

        if (reachedLastWave && EnemiesAlive <= 0)
        {
            //Go to next level
            gameManager.WinLevel();
            this.enabled = false; //Disabling it so it doesnt continue infinitely
        }
    }

    public int EnemiesInGame() { return EnemiesAlive; }

    //using coroutine to create a delay
    IEnumerator SpawnWave()
    {
        coroutineRunning = true;
        isSpawning = true;
        roundStatus.text = "ENEMIES SPAWNING...";

        wave = waves[waveIndex];

        EnemiesAlive = wave.enemyList.Count; //Sets the total number enmies here, so we dont have problems during the gaps between waves
                                             //^(so it doesnt automatically add a wave or switch levels because it thinks that it is done)



        foreach (var enemy in wave.enemyList)
        {
            SpawnEnemy(enemy.gameObject);

            //add delay so they dont all spawn at once
            yield return new WaitForSeconds(1f / wave.spawnRate);
        }

        tempIndex = waveIndex; //This was an attempt to stop the outofRange Exception but it didnt work..

        if ((tempIndex += 1) == waves.Length) //Compares our position versus the length
        {
            reachedLastWave = true;
        }
        else
        {
            waveIndex++;
        }
        tempIndex = 0;

        coroutineRunning = false;
        isSpawning = false;
        yield break; //adding just so it returns something no matter what
    }

    public bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0)
        {
            searchCountdown = .5f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null) { return false; }
        }//This is very taxing, best to do it on a timer
        return true;
    }

    void SpawnEnemy(GameObject _enemy)
    {
        Transform _sp = spawnPoint;

        //Spawn enemy
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }

    IEnumerator WaveInfoSetUp()
    {
        if (waveIndex + 1 > waves.Length)
        { //Trying to avoid null reference exception by pre-testing if it goes out of range - Still get it

        }
        else
        {
            Wave tempWave = waves[waveIndex];

            //could add one to current index
            Dictionary<string, int> enemyReadoutDict = tempWave.formEnemyTypeAndCount();

            string enemyReadoutString = "";
            //string enemyTypesString;

            //Go through dictionary and form it all into one long string
            foreach (var enemy in enemyReadoutDict)
            {
                enemyReadoutString = enemyReadoutString + " " + enemy.Key + ": " + enemy.Value.ToString() + ", ";
            }

            //Set text
            enemyReadoutInformation.text = enemyReadoutString; //Need to go get enemy types, and count out how many of each there are


            yield return new WaitForSeconds(5f);

            //Clear text
            enemyReadoutInformation.text = "";
            //yield return new WaitForSeconds(.5f); //Shows info for five seconds, then disappears
        }
    }
}
