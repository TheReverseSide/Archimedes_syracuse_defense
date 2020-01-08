using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform parentToReturn = null;
    public Transform placeHolderParent = null;
    //GameObject placeHolder = null;

    public enum CardType { ENDNEURON, HAND, NONE, GLIAL, MYELIN, GLUCOSE, TURRET }; //Would it be easier to use tags or card types?

    [Header("Card types")]
    public CardType cardType = CardType.NONE;
    public CardType recycleSlot = CardType.ENDNEURON;

    [Header("other stuff")]
    public Color colorBeforeHand;
    public GameObject body;
    public GameObject expander;
    public GameObject collapser;
    public CIED cied;

    public Component[] childComponents;




    public DropArea[] dropZones;

    void Start()
    {
        cied = this.GetComponent<CIED>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //HAVE TO IMPLEMENT TO ALL OTHER CARDS BEFORE USE
        //if (body.active == true)
        //{
        //    body.SetActive(false);
        //    expander.SetActive(true);
        //    collapser.SetActive(false);
        //}

        parentToReturn = this.transform.parent; //Saving old parent
        this.transform.SetParent(this.transform.parent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;

        dropZones = GameObject.FindObjectsOfType<DropArea>(); //Find all of (applicable) drop zones and then can make them glow (or appear)

        //Debug.Log("Make sure this matches: " + this.name);

        ChangeColor();
    }


    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position; //This is where DragDisappear breaks down

        //Debug.Log("dragging...");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent(parentToReturn); //Setting parent back to where we came from

        GetComponent<CanvasGroup>().blocksRaycasts = true;

        ChangeColorBack();
    }

    public void ChangeColor()
    {
        foreach (var dropZone in dropZones)
        {

            if (dropZone.GetComponent<GraphicRaycaster>() != null)
            {
                dropZone.GetComponent<GraphicRaycaster>().enabled = true;
            }

            dropZone.GetComponent<Renderer>().material = dropZone.beforeHandMat;

            if ((dropZone.typeOfCardAcceptedOne.Equals(cardType) || dropZone.typeOfCardAcceptedTwo.Equals(cardType)) && !dropZone.name.Equals("CardHand"))
            { //If it matches our type, make it glow!
                Material materialToChange = null;


                materialToChange = dropZone.colorToChange;


                if (dropZone.GetComponent<Renderer>() != null)
                {
                    dropZone.beforeHandMat = dropZone.GetComponent<Renderer>().material;

                    if (materialToChange != null)
                    {
                        // Debug.Log("Changing");
                        dropZone.NodeObject.GetComponent<Renderer>().material = materialToChange;

                        if (dropZone.Connector != null)
                        {
                            dropZone.Connector.GetComponent<Renderer>().material = materialToChange;
                            if (dropZone.ConnectorSecond != null)
                            {
                                dropZone.ConnectorSecond.GetComponent<Renderer>().material = materialToChange;

                                if (dropZone.ConnectorThird != null)
                                {
                                    dropZone.ConnectorThird.GetComponent<Renderer>().material = materialToChange;

                                    if (dropZone.ConnectorFour != null)
                                    {
                                        dropZone.ConnectorFour.GetComponent<Renderer>().material = materialToChange;
                                    }
                                }
                            }
                        }
                    }

                    //colorBeforeHand = dropZone.image.GetComponent<Image>().color;
                    //dropZone.GetComponent<Image>().color = new Color32(255, 41, 255, 100);
                }
                else
                {
                    foreach (Transform child in this.transform)
                    {
                        if (child.GetComponent<Image>() != null)
                        {
                            dropZone.beforeHandMat = dropZone.GetComponent<Renderer>().material;

                            if (materialToChange != null)
                            {
                                dropZone.GetComponent<Renderer>().material = materialToChange;
                            }
                        }
                    }
                }
            }

            /* if ()
             { //If it matches our type, make it glow!
                 Material materialToChange = null;

                 if (this.name.Contains("Turret") || this.name.Contains("turret"))
                 {
                     materialToChange = dropZone.highlightedMatTurret;
                 }
                 else if (this.name.Contains("Glial") || this.name.Contains("glial"))
                 {
                     materialToChange = dropZone.highlightedMatGlial;
                 }

                 if (dropZone.GetComponent<Renderer>() != null)
                 {
                     beforeHandMat = dropZone.GetComponent<Renderer>().material;

                     if (materialToChange != null)
                     {
                         //Debug.Log("Changing");
                         dropZone.GetComponent<Renderer>().material = materialToChange;

                         childComponents = GetComponentsInChildren<MeshRenderer>();

                         foreach (MeshRenderer child in childComponents)
                         {
                             child.material = materialToChange;
                         }
                     }

                     //colorBeforeHand = dropZone.image.GetComponent<Image>().color;
                     //dropZone.GetComponent<Image>().color = new Color32(255, 41, 255, 100);
                 }
                 else
                 {
                     foreach (Transform child in this.transform)
                     {
                         if (child.GetComponent<Image>() != null)
                         {
                             beforeHandMat = dropZone.GetComponent<Renderer>().material;

                             if (materialToChange != null)
                             {
                                 dropZone.GetComponent<Renderer>().material = materialToChange;
                             }
                         }
                     }
                 }
             } */
            if (dropZone.recycleBinType.Equals(recycleSlot)) //LIght up recycle areas
            {

                Material materialToChange = null;

                if (dropZone.GetComponent<Renderer>() != null)
                {
                    //Debug.Log("found renderer" + dropZone.gameObject.name);

                    dropZone.beforeHandMat = dropZone.GetComponent<Renderer>().material;
                    materialToChange = dropZone.colorToChange;

                    if (materialToChange != null)
                    {
                        dropZone.GetComponent<Renderer>().material = materialToChange;
                    }

                    //colorBeforeHand = dropZone.image.GetComponent<Image>().color;
                    //dropZone.GetComponent<Image>().color = new Color32(255, 41, 255, 100);
                }
                else
                {
                    foreach (Transform child in this.transform)
                    {
                        if (child.GetComponent<Image>() != null)
                        {
                            dropZone.beforeHandMat = dropZone.GetComponent<Renderer>().material;
                            materialToChange = dropZone.colorToChange;

                            if (materialToChange != null)
                            {
                                dropZone.GetComponent<Renderer>().material = materialToChange;
                            }
                        }
                    }
                }
            }
        }
    }

    public void ChangeColorBack() //The canvases will always be clear so it doesnt matter the color beforehand
    {
        // Can also use: EventSystem.current.RaycastAll(eventData); to send out raycasts to all objects under the card to determine what is under it and if it can be used
        foreach (var dropZone in dropZones)
        {


            if (dropZone.NodeObject != null)
            {
                dropZone.NodeObject.GetComponent<Renderer>().material = dropZone.beforeHandMat;
                if (dropZone.Connector != null)
                {
                    dropZone.Connector.GetComponent<Renderer>().material = dropZone.beforeHandMat;
                    if (dropZone.ConnectorSecond != null)
                    {
                        dropZone.ConnectorSecond.GetComponent<Renderer>().material = dropZone.beforeHandMat;

                        if (dropZone.ConnectorThird != null)
                        {
                            dropZone.ConnectorThird.GetComponent<Renderer>().material = dropZone.beforeHandMat;

                            if (dropZone.ConnectorFour != null)
                            {
                                dropZone.ConnectorFour.GetComponent<Renderer>().material = dropZone.beforeHandMat;
                            }
                        }
                    }
                }
            }


            //dropZone.GetComponent<Image>().color = new Color32(255, 255, 255, 100);

            if (dropZone.GetComponent<GraphicRaycaster>() != null && !dropZone.CompareTag("CardHand"))
            {
                dropZone.GetComponent<GraphicRaycaster>().enabled = false;
            }

        }
    }
}



