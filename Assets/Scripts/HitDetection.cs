using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
   BoxCollider boxCollider;
    
   void Awake(){
       // add isTrigger
       boxCollider = gameObject.GetComponent<BoxCollider>();
       boxCollider.isTrigger = true;
   }

   private void OnTriggerEnter(Collider other) {
       string tag = other.tag;
       
       Debug.Log("Trigger enter");

       if(tag.Equals("Wall")){
           //Nothing
           //Debug.Log("wall on wall collision - friendly fire");
       }else if(tag.Equals("Enemy")){
           Debug.Log("Troops disembark, big health drop");
           PlayerStats.health -= 3;
           Debug.Log(PlayerStats.health);
           other.gameObject.GetComponent<BoxCollider>().enabled = false;
           Destroy(other.gameObject, 2f);
           Debug.Log(PlayerStats.health);
       }else if (tag.Equals("Projectile"))
       {
        //    Debug.Log("Projectile impact");
        //    Debug.Log("Health" + PlayerStats.health);
           PlayerStats.health--;
       }
   }

   public void TakeDamage(float amount)
   {
       PlayerStats.health -= amount;

       if (PlayerStats.health <= 0)
       {
           //Game over
           Debug.Log("You dead");
       }
   }
}
