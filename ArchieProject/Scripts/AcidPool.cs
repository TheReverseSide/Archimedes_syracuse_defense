using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidPool : MonoBehaviour
{

    Rigidbody rgbd;

    public Material enemyBeforeHandMat;
    public Material enemyInAcidMat;

    bool standingInAcid;

    float normalEnemyMultiplier = 1f;
    float fastEnemyMultiplier = 1f;
    float toughEnemyMultiplier = 1f;
    float etherealEnemyMultiplier = 1f;
    float biophageEnemyMultiplier = 1f;
    float swarmEnemyMultiplier = 1f;

    float lerpDuration = 5.0f;

    float acidDamage = 10f;

    float timer;
    float initialTimer = 2.5f;

    void Start()
    {
        timer = initialTimer;
    }

    private void Update()
    {
        if (standingInAcid)
        {
            //Debug.Log("standingInAcid, subtracting time");
            timer -= Time.deltaTime;
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        SortEnemyAndDealDamage(collision);

        enemyBeforeHandMat = collision.gameObject.GetComponent<Renderer>().material;
        Debug.Log(enemyBeforeHandMat);

        Material[] materials = collision.gameObject.GetComponent<Renderer>().materials;
        materials[0] = enemyInAcidMat;
        materials[1] = enemyBeforeHandMat;
        collision.gameObject.GetComponent<Renderer>().materials[0] = materials[0];
        collision.gameObject.GetComponent<Renderer>().materials[1] = materials[1];

        Debug.Log(materials);

        enemyBeforeHandMat = collision.gameObject.GetComponent<Renderer>().material;
    }

    private void OnCollisionStay(Collision collision)
    {
        standingInAcid = true;

        collision.gameObject.GetComponent<Renderer>().material = enemyInAcidMat;

        if (timer <= 0)
        {
            Debug.Log("timer up, doing damage");

            SortEnemyAndDealDamage(collision);

            timer = initialTimer;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        standingInAcid = false;
        Debug.Log(standingInAcid);

        timer = initialTimer;

        //Need to implement some timer, somehow

        //Turn it back quickly
        Material[] materials = collision.gameObject.GetComponent<Renderer>().materials;
        materials[0] = enemyBeforeHandMat;
        materials[1] = null;
        collision.gameObject.GetComponent<Renderer>().materials[0] = materials[0];
        collision.gameObject.GetComponent<Renderer>().materials[1] = materials[1];

        //float lerp = lerpDuration; // Mathf.PingPong(Time.time, lerpDuration) 
        //collision.gameObject.GetComponent<Renderer>().material.Lerp(enemyInAcidMat, enemyBeforeHandMat, 1f);
    }

    void SortEnemyAndDealDamage(Collision collision)
    {
        Debug.Log(collision);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            foreach (Transform child in collision.gameObject.transform)
            {
                if (child.CompareTag("EnemyFast"))
                {
                    enemy.TakeDamage(acidDamage * fastEnemyMultiplier);

                    //Add some sort of smoke or bubbling effect, maybe tint enemies a color
                }
                else if (child.CompareTag("EnemySimple"))
                {
                    enemy.TakeDamage(acidDamage * normalEnemyMultiplier);
                }
                else if (child.CompareTag("EnemyTough"))
                {
                    enemy.TakeDamage(acidDamage * toughEnemyMultiplier);
                }
                else if (child.CompareTag("SwarmEnemy"))
                {
                    enemy.TakeDamage(acidDamage * swarmEnemyMultiplier);
                }
                else if (child.CompareTag("EtherealEnemy"))
                {
                    enemy.TakeDamage(acidDamage * etherealEnemyMultiplier);
                }
                else if (child.CompareTag("BioPhageEnemy"))
                {
                    enemy.TakeDamage(acidDamage * biophageEnemyMultiplier);
                }
            }
        }
    }
}
