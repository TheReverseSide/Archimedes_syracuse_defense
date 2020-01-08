using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glucose : MonoBehaviour
{
    //Needs to have a coroutine to provide (visually) glucose to the player
    //Must be marked as a target, have health, where enemies destory it
    //Should enemies have some attack protocol? 

    //Renderer rend;

    float health;
    float startHealth = 100f;

    bool isDestroyed;


    private void Start()
    {
        //rend = this.GetComponent<Renderer>();
        StartCoroutine(GenerateMoney());
        health = startHealth;
    }

    private void FixedUpdate()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    IEnumerator GenerateMoney()
    {
        while (!isDestroyed)
        {
            PlayerStats.currentGlucose += 10;
            //Debug.Log("Generating money: " + PlayerStats.currentGlucose);
            //this.GetComponent<Animation>().Play("answerResponseReveal");//Call to begin the anim that lasts ten seconds - Add some sort of "charging up (PvZ sunflower" animation to make it look nice?
            //this.GetComponentInChildren<Animation>().Play();

            Debug.Log("giving glucose");
            //Add money spawn effect here? I imagine some type of strong white point light gradually warming up then 'bursting'
            yield return new WaitForSeconds(10f);
        }
    }

    public void TakeDamage(float amount)
    {//public so it can be referenced from the Bullet class
        health -= amount;
        //healthBar.fillAmount = health / startHealth; //gives you the percentage in the format needed (0-1)

        Debug.Log("taking damage");

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
