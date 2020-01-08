using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeShopUI : MonoBehaviour
{

    public GameObject nUI; //The UI canvas containing upgrade, sell, prioritize buttons
    public GameObject ClickOffCanvas;

    public GameObject expandedCard;
    public GameObject hideCardButton;

    public Turret turret;

    public Shop shop;

    //Keeps track of current target (the node for which we are displaying the UI)
    private Node target;

    public void Start()
    {
        turret = GetComponent<Turret>();
        shop = GetComponent<Shop>();
    }

    public void SetTarget(Node _target)
    {
        this.target = _target;
        transform.position = target.getBuildPosition(); //Moves our UI to the build position by receiving node from build manager (who receives it from Node)

        nUI.SetActive(true);
        ClickOffCanvas.SetActive(true);

        if (expandedCard != null)
        {
            expandedCard.SetActive(false);
        }

        hideCardButton.SetActive(false);
        //Here ADD A CHECK FOR ADEQUATE MONEY SOMEWHERE - Go through for all turrets

    }

    public void Hide()
    {
        nUI.SetActive(false);
        ClickOffCanvas.SetActive(false);
        expandedCard.SetActive(false);
        hideCardButton.SetActive(false);
    }

    public void createTurret(TurretBlueprint turret)
    {
        if (CheckCost(turret))
        {
            target.BuildTurret(turret);

            nUI.SetActive(false);
            ClickOffCanvas.SetActive(false);
        }
        else
        {
            Debug.Log("You odnt have enough glucose");
        }
    }

    public bool CheckCost(TurretBlueprint turret)
    {
        return PlayerStats.currentGlucose > turret.cannonBaseCost;
    }
}
