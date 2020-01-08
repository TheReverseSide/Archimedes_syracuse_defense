using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlowTurret : MonoBehaviour
{

    public int effectRadius = 10;
    public GameObject effect;
    //public float slowPercent = .1f;
    //private int startSpeed;

    //Slows enemy with each pulse shot out
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 1f); //repeats named method after 0s wait, repeats every .3 second
    }

    // Update is called once per frame
    void UpdateTarget()
    {
        Quaternion target = Quaternion.Euler(90, 0, 0);
        GameObject slowEFfect = Instantiate(effect, transform.position, target);

        Destroy(slowEFfect, 2.5f);

        Collider[] colliders = Physics.OverlapSphere(transform.position, effectRadius);

        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Enemy"))
            {
                Slow(col.transform);
            }

            if (col.CompareTag("Projectile") && gameObject.CompareTag("VampireTurret"))
            {
                Vampire(col.transform); // need to apply colliders to all projectiles and add tag
            }
        }
    }

    void Slow(Transform enemy) //Changes all speeds to 3 - could this backfire with slow, strong enemies?
    {
        //I have to update manually -  it is the only solution I can think of right now
        NavMeshAgent e = enemy.GetComponent<NavMeshAgent>();
        e.speed = 3;

        //AudioManager.instance.Play("SlowTurret"); Need to find sound
    }

    void Vampire(Transform bullet) //Damage increased by 25% for every second the projectile spends in the vampire radius
    {
        Bullet b = bullet.GetComponent<Bullet>();
        b.damageDefault *= 1.25f;

        //AudioManager.instance.Play("VampireTurret"); Need to find sound
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, effectRadius);
    }
}
