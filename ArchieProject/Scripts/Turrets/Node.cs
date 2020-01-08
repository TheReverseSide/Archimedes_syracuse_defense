using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    //responsible for keeping track of whether or not things are built on the node, 
    //and user input, click input, etc

    #region Variables
    public Color hoverColor;
    public Color notEnoughGlucoseColor;
    public Vector3 positionOffet;
    public Shop shop;
    public GameObject buildPoint;

    [HideInInspector]
    public GameObject turret;

    [HideInInspector]
    public TurretBlueprint turretBlueprint;

    [HideInInspector]
    public bool isUpgradedOne = false;
    [HideInInspector]
    public bool isUpgradedTwo = false;

    private Renderer rend;
    private Color stockColor;

    ElectricGlial electricGlial;

    public RectTransform rectangle;

    BuildManager buildmanager;
    #endregion Variables

    void Start()
    {
        rend = GetComponent<Renderer>();
        buildmanager = BuildManager.instance; //accessing static instance variable
        stockColor = rend.material.color;
        //buildPoint = this.transform.position;
    }

    private void OnMouseEnter()
    {
        //if (EventSystem.current.IsPointerOverGameObject() || !buildmanager.CanBuild) //
        //return; //prevents us from selecting things below our UI elements && Stops us from doing anything if there is no current turrent to build

        if (turret == null) rend.material.color = hoverColor; //buildmanager.HasEnoughGlucose && 
                                                              //^goes and gets first material (tehre is only one) attached to game object, then changes its color

        //else if ((!buildmanager.HasEnoughGlucose) && turret == null)//This should be changed to color the UI according to what turrets the user can afford
        //{
        //    rend.material.color = notEnoughGlucoseColor;
        //}
        else rend.material.color = rend.material.color;
    }

    private void OnMouseDown()//when mouse is clicked while hovering
    {
        //if (EventSystem.current.IsPointerOverGameObject()) return; //prevents us from selecting things below our UI elements

        if (turret != null) //checking if there is already a turrent on the node - now if there is, we select it
        {
            buildmanager.SelectNode(this); //we pass in this node to select
            return;
        }
        else
        { //There is not a turret, open the shop menu
            buildmanager.nodeShopUI.SetTarget(this);
        }

        if (!buildmanager.CanBuild) return; //Stops us from doing anything if there is no current turrent to build

        BuildTurret(buildmanager.GetTurretToBuild());
    }

    private void OnMouseExit()
    {
        rend.material.color = stockColor;
    }

    public Vector3 getBuildPosition()
    {

        return buildPoint.transform.position + positionOffet;
    }

    public void BuildTurret(TurretBlueprint blueprint)
    {
        // buildManager.DeselectNode();

        //THE BUTTON SHOULD NOT BE CLICKABLE IF THEY DONT HAVE ENOUGH

        //if (!buildmanager.HasEnoughGlucose)
        //{
        //    Debug.Log("You do not have enough glucose to biuld that turret G" + blueprint.cost + " required.");
        //    AudioManager.instance.Play("NotEnoughGlucose");
        //    return;
        //}

        //Create the build effect and then destory it after 5 seconds
        GameObject effect = Instantiate(buildmanager.buildEffect, getBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        AudioManager.instance.Play("PlacingTurret");

        //build a turret
        GameObject _turret = Instantiate(blueprint.cannonBase, getBuildPosition(), Quaternion.identity);
        turret = _turret;
        turret.GetComponent<Turret>().node = this;

        turretBlueprint = blueprint;

        //Subtract and display remaining glucose
        PlayerStats.currentGlucose -= blueprint.cannonBaseCost; //Purchase cost
    }

    public void SpawnTurretFromCard(GameObject turret)
    {

        //Create the build effect and then destory it after 5 seconds
        GameObject effect = Instantiate(buildmanager.buildEffect, getBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        AudioManager.instance.Play("PlacingTurret");

        //build a turret
        GameObject _turret = Instantiate(turret, getBuildPosition(), Quaternion.identity);
        turret = _turret;

        //turretBlueprint = blueprint;

        //Debug.Log("Turret spawned!");
    }

    #region CANNON UPGRADES
    public void SpawnRangeCannon()
    {


        //Get rid of old turret
        Destroy(turret);

        //Subtract and display remaining glucose
        PlayerStats.currentGlucose -= turretBlueprint.rangedCannonCost; //Purchase cost

        //build upgraded turret
        GameObject _turret = Instantiate(turretBlueprint.rangedCannon, getBuildPosition(), Quaternion.identity);
        turret = _turret;

        turret.GetComponent<Turret>().node = this;
    }

    public void SpawnFireRateCannon()
    {
        //Get rid of old turret
        Destroy(turret);

        //Subtract and display remaining glucose
        PlayerStats.currentGlucose -= turretBlueprint.fireRateCannonCost; //Purchase cost

        //build upgraded turret
        GameObject _turret = Instantiate(turretBlueprint.fireRateCannon, getBuildPosition(), Quaternion.identity);
        turret = _turret;

        turret.GetComponent<Turret>().node = this;
    }

    public void SpawnMortarCannon()
    {
        //Get rid of old turret
        Destroy(turret);

        //Subtract and display remaining glucose
        PlayerStats.currentGlucose -= turretBlueprint.mortarCannonCost; //Purchase cost

        //build upgraded turret
        GameObject _turret = Instantiate(turretBlueprint.mortarCannon, getBuildPosition(), Quaternion.identity);
        turret = _turret;

        turret.GetComponent<Turret>().node = this;
    }
    #endregion

    #region GUN UPGRADES
    public void SpawnSniperRifle()
    {
        Debug.Log("Sniper rifle spawned!");

        //Get rid of old turret
        Destroy(turret);

        //Subtract and display remaining glucose
        PlayerStats.currentGlucose -= turretBlueprint.sniperRifleCost; //Purchase cost

        //build upgraded turret
        GameObject _turret = Instantiate(turretBlueprint.sniperRifle, getBuildPosition(), Quaternion.identity);
        turret = _turret;

        turret.GetComponent<Turret>().node = this;
    }

    public void SpawnGatlingGun()
    {
        Debug.Log("Gatling gun spawned!");

        //Get rid of old turret
        Destroy(turret);

        //Subtract and display remaining glucose
        PlayerStats.currentGlucose -= turretBlueprint.gatlingGunCost; //Purchase cost

        //build upgraded turret
        GameObject _turret = Instantiate(turretBlueprint.gatlingGun, getBuildPosition(), Quaternion.identity);
        turret = _turret;

        turret.GetComponent<Turret>().node = this;
    }

    public void SpawnPowerRifle()
    {
        Debug.Log("Power rifle spawned!");

        //Get rid of old turret
        Destroy(turret);

        //Subtract and display remaining glucose
        PlayerStats.currentGlucose -= turretBlueprint.powerGunCost; //Purchase cost

        //build upgraded turret
        GameObject _turret = Instantiate(turretBlueprint.powerGun, getBuildPosition(), Quaternion.identity);
        turret = _turret;

        turret.GetComponent<Turret>().node = this;
    }
    #endregion

    #region LASER UPGRADES
    #endregion

    #region ROCKET UPGRADES
    public void SpawnSwarmRocket()
    {
        Debug.Log("Swarm rockets spawned!");

        //Get rid of old turret
        Destroy(turret);

        //Subtract and display remaining glucose
        PlayerStats.currentGlucose -= turretBlueprint.swarmRocketCost; //Purchase cost

        //build upgraded turret
        GameObject _turret = Instantiate(turretBlueprint.swarmRocket, getBuildPosition(), Quaternion.identity);
        turret = _turret;

        turret.GetComponent<Turret>().node = this;
    }

    public void SpawnBurstRocket()
    {
        Debug.Log("Burst rockets spawned!");

        //Get rid of old turret
        Destroy(turret);

        //Subtract and display remaining glucose
        PlayerStats.currentGlucose -= turretBlueprint.burstRocketCost; //Purchase cost

        //build upgraded turret
        GameObject _turret = Instantiate(turretBlueprint.burstRocket, getBuildPosition(), Quaternion.identity);
        turret = _turret;

        turret.GetComponent<Turret>().node = this;
    }

    public void SpawnClusterRocket()
    {
        Debug.Log("Cluster rockets spawned!");

        //Get rid of old turret
        Destroy(turret);

        //Subtract and display remaining glucose
        PlayerStats.currentGlucose -= turretBlueprint.clusterRocketCost; //Purchase cost

        //build upgraded turret
        GameObject _turret = Instantiate(turretBlueprint.clusterRocket, getBuildPosition(), Quaternion.identity);
        turret = _turret;

        turret.GetComponent<Turret>().node = this;
    }
    #endregion

    public void SellTurret()
    {
        PlayerStats.currentGlucose += turretBlueprint.GetSellAmount();

        GameObject effect = Instantiate(buildmanager.sellEffect, getBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        AudioManager.instance.Play("SellTurret");

        Destroy(turret);

        turretBlueprint = null; //So we no longer have a selected turret
    }

    public bool ReturnsIfThereIsTurret()
    {
        return (turret != null);
    }

    public Turret ReturnTurret()
    {
        return turret.GetComponent<Turret>();
    }

    ////TURRET EFFECTS
    //public void ElectricTurretEffect()
    //{// 
    //    if (turret != null)
    //    {

    //    }
    //    else
    //    {
    //        Debug.Log("You lack a turret");
    //    }
    //}
    //public void CsfTurretEffect()
    //{//Adds some sort of acidic effect to the associated glial attach area
    //    if (turret != null)
    //    {

    //    }
    //    else
    //    {
    //        Debug.Log("You lack a turret");
    //    }
    //}
    //public void SlowTurretEffect()
    //{//Slows enemies that pass through the associated glial attach area (would have to be relative large to be worth it OR adds a slow effect to turret projectiles?
    //    if (turret != null)
    //    {

    //    }
    //    else
    //    {
    //        Debug.Log("You lack a turret");
    //    }
    //}

}
