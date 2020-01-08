using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    float spitCountDown = 0f; //Spits immediately
    float acidSpawnRate = 5f;

    public GameObject acidPool;
    public GameObject roughSpawnPoint;

    //Could I go get size of acidPool object then use that as a position offset? + thickness of walls?

    void Start()
    {
        float xSize = acidPool.transform.localScale.x;
        float ySize = acidPool.transform.localScale.y;
        float zSize = acidPool.transform.localScale.z;
    }

    void Update()
    {
        spitCountDown -= Time.deltaTime;

        if (spitCountDown <= 0)
        {
            SpitAcid();

            spitCountDown = 1f / acidSpawnRate; //Reset
        }
    }

    IEnumerator SpitAcid()
    {

        //Instantiate some sort of acid that stays on the ground and burns enemies that walk over it
        Instantiate(acidPool, roughSpawnPoint.transform);
        //Need some sort of animation that makes it look like it is spilling over the floor

        yield return new WaitForSeconds(10f); //Wait for acid to burn, then burn away, small wait, then spit again
    }
}
