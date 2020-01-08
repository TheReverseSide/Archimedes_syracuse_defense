using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipKiller : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        string tag = other.tag;

        if (tag.Equals("Wall"))
        {
            //Nothing
        }
        else if (tag.Equals("Enemy"))
        {
            Debug.Log("Hit ship");
            Destroy(other.gameObject);
        }
        else if (tag.Equals("Projectile"))
        {
            Debug.Log("Projectile impact");
        }
    }
}
