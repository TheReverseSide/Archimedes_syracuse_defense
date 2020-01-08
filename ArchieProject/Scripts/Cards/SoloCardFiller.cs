using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoloCardFiller : MonoBehaviour
{

    public Image cardImage;

    [Header("Name and desc")]
    public Text cardName;
    public Text cardDesc;
    public Text upgradeStatus;

    [Header("Stats")]
    public Text damage;
    public Text range;
    public Text splashRadius;

    BuildManager buildManager;

    //This should work by using the name (?) of the card to go and find the correct turret from the build manager

    void Start()
    {
        buildManager = FindObjectOfType<BuildManager>(); //There is always one, and only one, so this should work

        Debug.Log("found the build manager");
        Debug.Log(name);

        if (name.Contains("Cannon") || name.Contains("cannon"))
        {
            if (name.Equals("CannonTurret_Base_Card"))
            {
                Debug.Log("trying to fill data");
                GetCannonBaseData();
            }
            //else if(){

            //}
        }
        else if (cardImage.name.Contains("Gun") || cardImage.name.Contains("gun"))
        {

        }

    }

    void GetCannonBaseData()
    {
        cardName.text = buildManager.publicTurretList.cannonBase.GetComponent<Turret>().turretName;
        cardDesc.text = buildManager.publicTurretList.cannonBase.GetComponent<Turret>().turretDescription;
        upgradeStatus.text = buildManager.publicTurretList.cannonBase.GetComponent<Turret>().upgradeStatus;

        cardImage.sprite = buildManager.publicTurretList.cannonBase.GetComponent<Turret>().turretImage;

        range.text = buildManager.publicTurretList.cannonBase.GetComponent<Turret>().range.ToString();

        if (buildManager.publicTurretList.cannonBase.GetComponent<Turret>().projectilePrefab.GetComponent<Bullet>().damageMiddle > 0f)
        { //It has splash damage, display it
            damage.text = "(" + buildManager.publicTurretList.cannonBase.GetComponent<Turret>().projectilePrefab.GetComponent<Bullet>().damageDefault.ToString() + ", " + buildManager.publicTurretList.cannonBase.GetComponent<Turret>().projectilePrefab.GetComponent<Bullet>().damageMiddle.ToString() + ", " + buildManager.publicTurretList.cannonBase.GetComponent<Turret>().projectilePrefab.GetComponent<Bullet>().damageOuter.ToString() + ")";
            splashRadius.text = "(" + buildManager.publicTurretList.cannonBase.GetComponent<Turret>().projectilePrefab.GetComponent<Bullet>().explosionRadiusCenter.ToString() + ", " + buildManager.publicTurretList.cannonBase.GetComponent<Turret>().projectilePrefab.GetComponent<Bullet>().explosionRadiusMiddle.ToString() + ", " + buildManager.publicTurretList.cannonBase.GetComponent<Turret>().projectilePrefab.GetComponent<Bullet>().explosionRadiusOuter.ToString() + ")";
        }
        else
        {
            damage.text = buildManager.publicTurretList.cannonBase.GetComponent<Turret>().bulletDamage.ToString();
        }
    }


}
