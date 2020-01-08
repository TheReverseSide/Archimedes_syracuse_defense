using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable] //to be able to see the fields in the inspector
public class TurretBlueprint
{
    [Header("CIED Pick Me")]
    public GameObject thisCIEDturret;
    public int thisCIEDTurretCost;

    [Header("Originals")]
    public GameObject cannonBase;
    public int cannonBaseCost = 100;

    public GameObject laserBase;
    public int laserBaseCost;

    public GameObject gunBase;
    public int gunBaseCost;

    public GameObject rocketBase;
    public int rocketBaseCost;

    [Header("Cannon variants")]
    public GameObject rangedCannon;
    public int rangedCannonCost;

    public GameObject fireRateCannon;
    public int fireRateCannonCost;

    public GameObject mortarCannon;
    public int mortarCannonCost;

    [Header("Rocket variants")]
    public GameObject swarmRocket;
    public int swarmRocketCost;

    public GameObject burstRocket;
    public int burstRocketCost;

    public GameObject clusterRocket;
    public int clusterRocketCost;

    [Header("Gun variants")]
    public GameObject sniperRifle;
    public int sniperRifleCost;

    public GameObject gatlingGun;
    public int gatlingGunCost;

    public GameObject powerGun;
    public int powerGunCost;

    //[Header("Laser variants")]

    public int GetSellAmount()
    {
        return (int)(cannonBaseCost / 2);
    }
}
