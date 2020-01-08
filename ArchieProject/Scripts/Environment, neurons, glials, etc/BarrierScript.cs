using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierScript : MonoBehaviour
{

    //Renderer rend;

    float health;
    public float startHealth;

    bool isDestroyed;


    private void Start()
    {
        //rend = this.GetComponent<Renderer>();
        health = startHealth;
    }

    private void FixedUpdate()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float amount)
    {//public so it can be referenced from the Bullet class
        health -= amount;
        //healthBar.fillAmount = health / startHealth; //gives you the percentage in the format needed (0-1)

        if (health <= 0 && isDestroyed == false)
        { Die(); }
    }

    void Die()
    {
        //Add a death animation
        isDestroyed = true;
        Destroy(gameObject);
    }
}
