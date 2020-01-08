using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricGlial : MonoBehaviour
{

    GameObject turret;
    Bullet bullet;

    Material originalMaterial;
    public Material electricMaterial;

    private void Start()
    {
        turret = this.GetComponent<Node>().turret;
        turret.GetComponent<Turret>().electricActivated = true;

        bullet = turret.GetComponent<Turret>().bullet;
        originalMaterial = bullet.GetComponent<Renderer>().material;
    }


    public Bullet ElectrifyEffect(Bullet bullet)
    {
        //Turns projectiles blue
        bullet.GetComponent<Renderer>().material = electricMaterial;

        //Temporarily stuns units - disables enemies with special abilities? Could make every 5 rounds an electric one
        Debug.Log("should add effect to bullet");

        bullet.impactElectricEffect = true;
        //Later I could add a minor chain effect (an upgrade?)

        return bullet;
    }
}
