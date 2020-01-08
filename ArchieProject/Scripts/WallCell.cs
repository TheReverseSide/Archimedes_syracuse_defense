using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCell : MonoBehaviour
{
    float movementSpeed;

    float health;
    public float startHealth;

    Enemy targetEnemy;
    Transform target;
    Vector3 initialPos;

    bool isDestroyed;

    public GameObject spawnArea;
    public float attackRange;
    public float attackDamage;

    public GameObject[] enemies;

    public GameObject impactEffect;


    private void Start()
    {
        //rend = this.GetComponent<Renderer>();
        health = startHealth;

        initialPos = this.transform.position;

        movementSpeed = Random.Range(3f, 5f);

        InvokeRepeating("UpdateTarget", 0f, 0.25f);
    }

    private void FixedUpdate()
    {
        if (health <= 0)
        {
            Die();
        }
        else if (target != null) //If there is any enemy nearby, attack it
        {
            SeekTarget();
        }
        else if (target == null) //otherwise, stick around a certain position
        {
            //If there was an enemy (so they are bunched up now) they shoudl reorganize and wait

            //Navigate to initial Pos
            //NavigateToPos();
        }
    }


    void Die()
    {
        //Add a death animation
        isDestroyed = true;
        Destroy(gameObject);
    }

    public void UpdateTarget()
    {
        //distance checks and searching through all objects takes computer power, so it is better to have it run twice a second or so 
        //renewed search through all objects tagged as enemy, finds closest, checks if its within range, then sets target 

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float closestDistance = Mathf.Infinity; //Using infinity so we dont accidentally make it too big

        if (target != null && (DistanceToEnemy(this.transform.position, target.transform.position)) <= attackRange) //if staylocked is on and there is currently a target and they are within range, keep it that way
        {
            return;
        }

        //Otherwise clear enemy and re-evalutate closest target
        GameObject closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = DistanceToEnemy(this.transform.position, enemy.transform.position);

            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null && closestDistance <= attackRange) //If there is an enemy and it is within range...
        {
            target = closestEnemy.transform; //..set it to the target
            targetEnemy = closestEnemy.GetComponent<Enemy>(); //sets targetEnemy to closest enemy and adds functionality from Enemy class (for takeDamage)
        }
        else
        {
            target = null;
        } //otherwise dont set a target
    }

    void SeekTarget()
    {

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = movementSpeed * Time.deltaTime;

        //O_ should there be a more efficient way to determine if we hit it?
        if (dir.magnitude <= distanceThisFrame) // if length (magnitude) of dir vector is less than or equal to distance
        { //needed to move this frame, then we hit something
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World); //normalized to make sure that we move at a constant speed (distanceThisFrame)
                                                                              //Move relative to world space, not local space to avoid weirdness 
                                                                              //Add rotation (for the missile to look at its target

        transform.LookAt(target);
    }

    void NavigateToPos()
    {
        Vector3 dir = initialPos - transform.position;
        float distanceThisFrame = movementSpeed * Time.deltaTime;

        transform.Translate(dir.normalized * distanceThisFrame, Space.World); //normalized to make sure that we move at a constant speed (distanceThisFrame)
                                                                              //Move relative to world space, not local space to avoid weirdness 
                                                                              //Add rotation (for the missile to look at its target
        if (dir.magnitude <= distanceThisFrame) // if length (magnitude) of dir vector is less than or equal to distance
        { //needed to move this frame, then we hit something

            return;
        }

        transform.LookAt(initialPos);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            target = collision.transform;
            HitTarget();
        }
    }

    void HitTarget()
    {
        Debug.Log("hit target");
        GameObject effectInstance = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 5f); //delay before destroying particle effects

        //Both do and take damage
        DoAndTakeDamage();
    }

    private float DistanceToEnemy(Vector3 thisTurret, Vector3 enemy)
    {
        return Vector3.Distance(thisTurret, enemy);
    }

    void DoAndTakeDamage()
    {
        Enemy e = target.GetComponent<Enemy>(); //called e to differentitate between this variable and the entire object, also called enemy
                                                //this gets enemy script from enemy prefab, attaching it to our e here for use


        if (e != null) //making sure there is an enemy to damage, in case we forget to label something as enemy
        { e.TakeDamage(attackDamage); }

        this.health -= e.deathDamage;

        if (health <= 0 && isDestroyed == false)
        { Die(); }

        //sound would go here
    }
}
