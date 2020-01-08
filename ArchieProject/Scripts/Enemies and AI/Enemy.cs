using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    #region Variables
    [HideInInspector]
    public float speed;

    [Header("Enemy stats")]
    public float startSpeed = 10f;
    public int glucoseValue = 50;
    public int pointsValue = 50;
    public float deathDamage = 0; //Damage done to player health upon death

    [Header("Health")]
    public float startHealth = 100f;

    [HideInInspector]
    public float health;

    [Header("Game Objects")]
    public GameObject FloatingGlucosePrefab;
    public GameObject deathEffect;
    public GameObject CentralNueronHit;

    //[Header("Unity Stuff")]


    [HideInInspector]
    public bool enemyDiesOnce;
    #endregion Variables

    private void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }

    public void OnCollisionEnter(Collision collision)
    {
        Transform parent = collision.gameObject.transform;

        if (this.name.Contains("phage"))
        {
            deathDamage *= 1.7f;
        }
        else if (this.name.Contains("ethereal"))
        {

        }

        for (int i = 0; i < parent.childCount; i++) //They should normally just have one child so this shouldnt pose a problem
        {
            if (parent.GetChild(i).gameObject.tag == "EndNeuron")
            {
                PathEnded();
            }
            else if (parent.GetChild(i).gameObject.tag == "Glucose")
            {
                //Glucose hit effect
                parent.GetComponent<Glucose>().TakeDamage(this.deathDamage);
            }
            else if (parent.GetChild(i).gameObject.tag == "Barrier")
            {
                //Glucose hit effect
                parent.GetComponent<BarrierScript>().TakeDamage(this.deathDamage);
            }
            else if (parent.GetChild(i).gameObject.tag == "WallCell")
            {
                //Glucose hit effect

                //Pretty sure the Wall Cell handles all of this, so it is not necessary here
                //parent.GetComponent<WallCell>().TakeDamage(this.deathDamage);
            }
        }
    }

    public void TakeDamage(float amount)
    {//public so it can be referenced from the Bullet class
        health -= amount;

        if (health <= 0 && enemyDiesOnce == false)
        { Die(); }
    }

    void Die()
    {
        //Prevents an enemy from dying more than once because Destory() can take a long time
        enemyDiesOnce = true;

        foreach (Transform child in transform)
        {
            if (child.CompareTag("EnemyFast"))
            {
                AudioManager.instance.Play("SwarmFastEnemyDeath");
            }
            else if (child.CompareTag("SwarmEnemy"))
            {
                AudioManager.instance.Play("SwarmFastEnemyDeath");
            }
            else if (child.CompareTag("EnemySimple"))
            {
                AudioManager.instance.Play("SimpleEnemyDeath");
            }
            else if (child.CompareTag("EnemyTough"))
            {
                AudioManager.instance.Play("ToughEnemyDeath");
            }

        }

        //Play dead effect then remove it after 5s
        GameObject deadEffect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(deadEffect, 5f);

        if (FloatingGlucosePrefab != null) //Instantiate floating glucose sign aftering checking that it is assigned
        {
            ShowFloatingGlucose();
        }

        //Give glucose for kill
        PlayerStats.currentGlucose += glucoseValue;

        //Add to enemies killed
        PlayerStats.enemiesKilled++;

        //Add to points
        PlayerStats.currentsPoints += pointsValue;

        //Remove enemy from scene
        Destroy(gameObject);

        WaveSpawner.EnemiesAlive--;
    }

    void ShowFloatingGlucose()
    {
        var gluc = Instantiate(FloatingGlucosePrefab, transform.position, Quaternion.identity); //I tink the last transform makes it a child of the enemy
        gluc.GetComponent<TextMesh>().text = this.glucoseValue + "";
    }

    public void Slow(float amountToSlow) //public because we will call it outside of the class
    {
        //speed = speed * (1f - amountToSlow); //This is a problem because the speed would keep decreasing as the frames update
        speed = startSpeed * (1f - amountToSlow); //this prevents that^ problem
    }

    void PathEnded()
    {
        GameObject hitEffect = Instantiate(CentralNueronHit, transform.position, Quaternion.identity);
        Destroy(hitEffect, .8f);

        AudioManager.instance.Play("CentralNodeHit");

        //Subtract from wahtever brain health I create here
        //brainHealth -= deathDamage;

        if (PlayerStats.Health + 1 < 100)
        { PlayerStats.Health = 100; }

        else PlayerStats.Health += deathDamage;

        WaveSpawner.EnemiesAlive--;

        Destroy(gameObject);

        return; //prevents us from going down below to other code because Destroy() can take a long time
    }
}
