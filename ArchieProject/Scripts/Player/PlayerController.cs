using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;
    public Transform target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("UpdateTarget", 0f, 0.2f); //repeats named method after 0s wait, repeats every .3 second
    }

    public void UpdateTarget()
    {
        GameObject[] potentialTargets = GameObject.FindGameObjectsWithTag("target"); //Creates an array of all game objects with enemy tag
        float closestDistance = Mathf.Infinity; //Using infinity so we dont accidentally make it too big


        if (target != null) //if staylocked is on and there is currently a target, keep it that way
        {
            return;
        }
        GameObject closestEnemy = null;

        foreach (GameObject pT in potentialTargets) //Cycle through all enemies found in array
        {
            float distanceToEnemy = Vector3.Distance(transform.position, pT.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = pT;
            }
        }

        if (closestEnemy != null) //If there is an enemy and it is within range...
        {
            target = closestEnemy.transform; //..set it to the target
            agent.SetDestination(target.transform.position);

            //targetEnemy = closestEnemy.GetComponent<Enemy>(); //sets targetEnemy to closest enemy and adds functionality from Enemy class (for takeDamage)
        }
        else { target = null; } //otherwise dont set a target
    }
}