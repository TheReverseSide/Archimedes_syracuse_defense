using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    //PROBLEMS:

    //It is deleting no matter where you drop it - GLucose only???
    //Not highlighting what it should when a card is held and hovering - Myelin works??? Could be in glucose code here
    // I also need to change color of children

    [Header("Accepted types")]
    public Draggable.CardType typeOfCardAcceptedOne = Draggable.CardType.ENDNEURON; //Acts as our filter - set in Unity
    public Draggable.CardType typeOfCardAcceptedTwo = Draggable.CardType.ENDNEURON;

    [Header("Recycling types")]
    public Draggable.CardType recycleBinType = Draggable.CardType.ENDNEURON; //Acts as our filter - set in Unity

    [HideInInspector]
    public Image image;

    public Node node;
    public Shop shop;
    public CIED cied;
    public Vector3 dropPositionOffset;

    public GameObject NodeObject;
    public GameObject Connector;
    public GameObject ConnectorSecond;
    public GameObject ConnectorThird;
    public GameObject ConnectorFour;

    [HideInInspector]
    public GameObject tempTurret;
    bool hasTurret;

    [Header("Materials")]

    public Material beforeHandMat;
    public Material colorToChange;

    [HideInInspector]
    public TurretBlueprint turretBlueprint;

    BuildManager buildmanager;

    bool isCannonBase;
    bool isRocketBase;
    bool isLaserBase;
    bool isGunBase;

    //bool isSlowGlial;
    bool isCSFGlial;
    bool isResistGlial;
    bool isElectricGlial;
    bool isSlowGlial;

    //Have to make a separate bunch of methods for "burning cards" at the end node
    //Glucose can be used only there - everything else is simply burnt

    private void Start()
    {
        buildmanager = BuildManager.instance;
        image = GetComponent<Image>();
        node = GetComponentInParent<Node>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Draggable draggable = eventData.pointerDrag.GetComponent<Draggable>();

        if (draggable != null)
        {
            draggable.ChangeColorBack();

            if (draggable.tag.Contains("turret") || draggable.tag.Contains("Turret")) //SORT FOR GLIALS HERE
            {
                if (draggable.tag.Contains("cannon") || draggable.tag.Contains("Cannon"))
                {
                    if (draggable.CompareTag("CannonTurret_Base_CIED"))
                    {
                        isCannonBase = true;

                    }
                    else if (draggable.CompareTag("CannonTurret_LongRange_CIED"))
                    {

                    }
                    else if (draggable.CompareTag("CannonTurret_FireRate_CIED"))
                    {

                    }
                    else if (draggable.CompareTag("CannonTurret_Mortar_CIED"))
                    {

                    }
                }
                else if (draggable.tag.Contains("gun") || draggable.tag.Contains("Gun"))
                {
                    if (draggable.CompareTag("GunTurret_Base_CIED"))
                    {
                        isGunBase = true;
                    }
                    //else if (draggable.CompareTag(""))
                    //{

                    //}
                    //else if (draggable.CompareTag(""))
                    //{

                    //}
                    //else if (draggable.CompareTag(""))
                    //{

                    //}
                }
                else if (draggable.tag.Contains("rocket") || draggable.tag.Contains("Rocket"))
                {
                    if (draggable.CompareTag("RocketTurret_Base"))
                    {
                        isRocketBase = true;
                    }
                    //else if (draggable.CompareTag(""))
                    //{

                    //}
                    //else if (draggable.CompareTag(""))
                    //{

                    //}
                    //else if (draggable.CompareTag(""))
                    //{

                    //}
                }
                else if (draggable.tag.Contains("laser") || draggable.tag.Contains("Laser"))
                {
                    if (draggable.CompareTag("LaserTurret_Base_CIED"))
                    {
                        isLaserBase = true;
                    }
                    //else if (draggable.CompareTag(""))
                    //{

                    //}
                    //else if (draggable.CompareTag(""))
                    //{

                    //}
                    //else if (draggable.CompareTag(""))
                    //{

                    //}
                }
            }
            else if (draggable.tag.Contains("glial") || draggable.tag.Contains("Glial")) //SORT FOR GLIALS HERE
            {
                if (draggable.CompareTag("electricGlialCard"))
                {
                    isElectricGlial = true;
                }
                else if (draggable.CompareTag("csfGlialCard"))
                {
                    isCSFGlial = true;
                }
                else if (draggable.CompareTag("slowGlialCard"))
                {
                    isSlowGlial = true;
                }
                //else if (draggable.CompareTag("resistanceGlialCard"))
                //{
                //isResistGlial = true;
                //}
            }


            //IT IS SOMETHING TO PLACE
            if (typeOfCardAcceptedOne == draggable.cardType || typeOfCardAcceptedTwo == draggable.cardType) //It is accepted and will be destroyed, find out what type
            {


                if (draggable.tag.Contains("glial") || draggable.tag.Contains("Glial")) //SORT FOR GLIALS HERE
                {
                    //NEED TO FIGURE OUT IF parent node has a turret, and if so, delete it and re-instantiate it at the correct point
                    if (this.transform.parent.GetComponent<Node>().ReturnsIfThereIsTurret())
                    {
                        tempTurret = this.transform.parent.GetComponent<Node>().turret;
                        hasTurret = true;
                    }

                    //Save position of Myelin node
                    Vector3 tempPos = this.transform.parent.localPosition;
                    Quaternion tempRot = this.transform.parent.localRotation;

                    Destroy(this.transform.parent.gameObject);
                    Destroy(tempTurret);

                    //Get the type of card to determine which effect to give
                    if (isSlowGlial)
                    {
                        GameObject slowGlial = Instantiate(buildmanager.slowGlialPrefab, tempPos + buildmanager.slowGlialPosOffset, tempRot);

                        if (hasTurret) //Spawns turret in correct position if one was there previously
                        {
                            foreach (Transform child in slowGlial.transform)
                            {
                                if (child.name.Contains("elin"))
                                { //
                                    //node.positionOffet = new Vector3(0, -.5f, 0);
                                    node.buildPoint = child.GetComponent<Node>().buildPoint; //Then I can set it to each individual glial?
                                }
                            }
                            Instantiate(tempTurret, this.transform.parent.GetComponent<Node>().getBuildPosition(), Quaternion.identity);
                        }
                    }
                    else if (isElectricGlial)
                    {

                    }
                    else if (isResistGlial)
                    {

                    }
                    else if (isCSFGlial)
                    {

                    }
                }
                else if (draggable.tag.Contains("turret") || draggable.tag.Contains("Turret"))
                {
                    GameObject turret;

                    if (isCannonBase)
                    {
                        turret = buildmanager.publicTurretList.cannonBase;
                    }
                    else if (isGunBase)
                    {
                        turret = draggable.cied.turret.gunBase;
                    }
                    else if (isLaserBase)
                    {
                        turret = draggable.cied.turret.laserBase;
                    }
                    else if (isRocketBase)
                    {
                        turret = draggable.cied.turret.rocketBase;
                    }
                    else
                    {
                        turret = null;
                        Debug.Log("no turret found");
                    }

                    node.SpawnTurretFromCard(turret);
                }
                else
                {
                    Debug.Log("Something is wrong and nothing was found, do something");
                }
                //Debug.Log("card should be removed - placed");
            }



            //IT IS SOMETHING TO RECYCLE
            if (recycleBinType == Draggable.CardType.ENDNEURON)
            {
                if (draggable.tag.Contains("glucose") || draggable.tag.Contains("Glucose"))
                {
                    //Debug.Log("burning glucose..");

                    if (draggable.tag.Contains("50"))
                    {
                        Debug.Log("Redeemed glucose card for 50 glucose");
                        PlayerStats.currentGlucose += 50;
                    }
                    else if (draggable.tag.Contains("100"))
                    {
                        Debug.Log("Redeemed glucose card for 100 glucose");
                        PlayerStats.currentGlucose += 100;
                    }
                    else if (draggable.tag.Contains("250"))
                    {
                        Debug.Log("Redeemed glucose card for 250 glucose");
                        PlayerStats.currentGlucose += 250;
                    }

                    GameObject effect = Instantiate(buildmanager.glucoseBurnEffect, transform.position, Quaternion.identity);
                    Destroy(effect, 5f);

                    //AudioManager.instance.Play("Burning glucose");
                }
                else if (isCSFGlial)
                {
                    Debug.Log("Burned card for 50 glucose");
                    PlayerStats.currentGlucose += 50;
                }
                else if (isElectricGlial)
                {
                    Debug.Log("Burned card for 50 glucose");
                    PlayerStats.currentGlucose += 50;
                }
                else if (isSlowGlial)
                {
                    Debug.Log("Burned card for 50 glucose");
                    PlayerStats.currentGlucose += 50;
                }
                else if (isResistGlial)
                {
                    Debug.Log("Burned card for 50 glucose");
                    PlayerStats.currentGlucose += 50;
                }
                else if (isCannonBase)
                {
                    Debug.Log("Burned card for 50 glucose");
                    PlayerStats.currentGlucose += 50;
                }
                else if (isLaserBase)
                {
                    Debug.Log("Burned card for 50 glucose");
                    PlayerStats.currentGlucose += 50;
                }
                else if (isRocketBase)
                {
                    Debug.Log("Burned card for 50 glucose");
                    PlayerStats.currentGlucose += 50;
                }
                else if (isGunBase)
                {
                    Debug.Log("Burned card for 50 glucose");
                    PlayerStats.currentGlucose += 50;
                }
                //O_ Call some burn effect that everything besides glucose

                //Debug.Log("card should be removed - recycled");
            }

            //this.GetComponent<Image>().color = new Color32(255, 255, 255, 0); //GO back to invisible

            //Debug.Log("before removing");

            //foreach (var item in PlayerStats.cardDeck)
            //{
            //    Debug.Log(item.name);
            //}

            //Debug.Log(draggable.name);

            //Try to remove
            //Debug.Log(PlayerStats.cardDeck);

            if (PlayerStats.cardDeck.Count > 0)
            {
                foreach (var item in PlayerStats.cardDeck)
                {
                    //Debug.Log("Item name : " + item.name);
                    if (draggable.name.Equals(item.name) || draggable.name.Equals(item.name + "(Clone)") || draggable.name.Equals(item.name + "(clone)"))
                    {
                        //Debug.Log("thinkwe found a match.. trying to delete");
                        PlayerStats.cardDeck.Remove(item);

                        Destroy(draggable.gameObject);

                        return; //to avoid deleting more than one

                    }
                }
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData) //Can be used to add an effect
    {
        if (eventData.pointerDrag == null) { return; }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) { return; }
    }
}
