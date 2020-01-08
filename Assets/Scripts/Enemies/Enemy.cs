using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public float startSpeed = 10f;

    [HideInInspector]
    public float speed;

    public float startHealth = 100;
    private float health;

    public int worth = 50;

    public GameObject deathEffect;

    [Header("Unity Stuff")]
    public Image healthBar;

    private bool isDead = false;

    void Start()
    {
        speed = startSpeed;
        health = startHealth;

        //Transform tMin = GetClosestEnemy();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        PlayerStats.Money += worth;

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        WaveSpawner.EnemiesAlive--;

        Destroy(gameObject);
    }

    //Transform GetClosestEnemy()
    //{
    //    Transform tMin = null;
    //    Transform[] enemies = FindObjectsOfType<Transform>();


    //    float minDist = Mathf.Infinity;
    //    Vector3 currentPos = this.gameObject.GetComponent<Transform>().position;
    //    foreach (Transform t in enemies)
    //    {
    //        if (t.gameObject.compareTag(""))
    //        float dist = Vector3.Distance(t.position, currentPos);
    //        if (dist < minDist)
    //        {
    //            tMin = t;
    //            minDist = dist;
    //        }
    //    }
    //    return tMin;
    //}

}
