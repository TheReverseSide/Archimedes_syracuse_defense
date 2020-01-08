using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardFiller : MonoBehaviour
{

    CIED cied;
    [HideInInspector]
    public TurretBlueprint turretBP;


    [Header("Name and desc")]
    public Text cardName;
    public Text cardDesc;
    public Text upgradeStatus;

    [Header("Stats")]
    public Text damage;
    public Text range;
    public Text splashRadius;

    [Header("Image")]
    public SpriteRenderer image;

    void Start()
    {
        //InvokeRepeating("UpdateData", 0f, 2f); //Check for updated data every two seconds.. is this viable? GetComponent may be taxing IIRC

        cied = this.GetComponentInParent<CIED>();

        turretBP = cied.turret;

        //I dont think this works anymore. it is searching for just one turret, not depending on the type (given by card)
        //What I need is a way for CIED to pick a certain turret, to be picked up back here
        //What if only one turret was palced in there, then we just had it go get the only instance of turret there was?
        cardName.text = turretBP.thisCIEDturret.GetComponent<Turret>().turretName;
        cardDesc.text = turretBP.thisCIEDturret.GetComponent<Turret>().turretDescription;
        upgradeStatus.text = turretBP.thisCIEDturret.GetComponent<Turret>().upgradeStatus;

        image.sprite = turretBP.thisCIEDturret.GetComponent<Turret>().turretImage;

        range.text = turretBP.thisCIEDturret.GetComponent<Turret>().range.ToString();

        if (turretBP.thisCIEDturret.GetComponent<Turret>().projectilePrefab.GetComponent<Bullet>().damageMiddle > 0f)
        { //It has splash damage, display it
            damage.text = "(" + turretBP.thisCIEDturret.GetComponent<Turret>().projectilePrefab.GetComponent<Bullet>().damageDefault.ToString() + ", " + turretBP.thisCIEDturret.GetComponent<Turret>().projectilePrefab.GetComponent<Bullet>().damageMiddle.ToString() + ", " + turretBP.thisCIEDturret.GetComponent<Turret>().projectilePrefab.GetComponent<Bullet>().damageOuter.ToString() + ")";
            splashRadius.text = "(" + turretBP.thisCIEDturret.GetComponent<Turret>().projectilePrefab.GetComponent<Bullet>().explosionRadiusCenter.ToString() + ", " + turretBP.thisCIEDturret.GetComponent<Turret>().projectilePrefab.GetComponent<Bullet>().explosionRadiusMiddle.ToString() + ", " + turretBP.thisCIEDturret.GetComponent<Turret>().projectilePrefab.GetComponent<Bullet>().explosionRadiusOuter.ToString() + ")";
        }
        else
        {
            damage.text = turretBP.thisCIEDturret.GetComponent<Turret>().bulletDamage.ToString();
        }
    }


    public void UpdateData() //Called when a turret is upgraded, etc to update card
    {
        cied = this.GetComponentInParent<CIED>();

        turretBP = cied.turret;

        cardName.text = turretBP.cannonBase.GetComponent<Turret>().turretName;
        cardDesc.text = turretBP.cannonBase.GetComponent<Turret>().turretDescription;
        upgradeStatus.text = turretBP.cannonBase.GetComponent<Turret>().upgradeStatus;

        image.sprite = turretBP.cannonBase.GetComponent<Turret>().turretImage;

        range.text = turretBP.cannonBase.GetComponent<Turret>().range.ToString();

        if (turretBP.cannonBase.GetComponent<Turret>().projectilePrefab.GetComponent<Bullet>().damageMiddle > 0f)
        { //It has splash damage, display it
            damage.text = "(" + turretBP.cannonBase.GetComponent<Turret>().projectilePrefab.GetComponent<Bullet>().damageDefault.ToString() + ", " + turretBP.cannonBase.GetComponent<Turret>().projectilePrefab.GetComponent<Bullet>().damageMiddle.ToString() + ", " + turretBP.cannonBase.GetComponent<Turret>().projectilePrefab.GetComponent<Bullet>().damageOuter.ToString() + ")";
            splashRadius.text = "(" + turretBP.cannonBase.GetComponent<Turret>().projectilePrefab.GetComponent<Bullet>().explosionRadiusCenter.ToString() + ", " + turretBP.cannonBase.GetComponent<Turret>().projectilePrefab.GetComponent<Bullet>().explosionRadiusMiddle.ToString() + ", " + turretBP.cannonBase.GetComponent<Turret>().projectilePrefab.GetComponent<Bullet>().explosionRadiusOuter.ToString() + ")";
        }
        else
        {
            damage.text = turretBP.cannonBase.GetComponent<Turret>().bulletDamage.ToString();
        }
    }
}
