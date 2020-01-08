using UnityEngine;

public class Shop : MonoBehaviour
{
    [Header("Cannons")]
    public TurretBlueprint turretBluePrint;
    public TurretBlueprint turretBluePrint2;

    TurretBlueprint turretToBuild;

    //public GameObject nUI;
    //public GameObject ClickOffCanvas;
    public NodeShopUI nodeShopUI;
    //bool isActivated = true;

    void Start()
    {
        nodeShopUI = this.GetComponent<NodeShopUI>();
        //buildManager = BuildManager.instance;
    }

    public void SelectCannonTurret()
    {
        turretToBuild = turretBluePrint;
        nodeShopUI.createTurret(turretToBuild);
        PlayerStats.currentGlucose -= 100;

    }

    public void SelectGunTurret()
    {
        turretToBuild = turretBluePrint2;
        nodeShopUI.createTurret(turretToBuild);
        PlayerStats.currentGlucose -= 100;
    }


}
