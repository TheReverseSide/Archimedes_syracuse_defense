using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragDisappear : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject header = null;
    public GameObject iconPanel = null;
    public Draggable draggable;


    void Start()
    {
        draggable = this.transform.parent.GetComponent<Draggable>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        header.SetActive(false);
        iconPanel.SetActive(false);
        draggable.OnBeginDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
        //draggable.OnDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        draggable.OnEndDrag(eventData);


        header.SetActive(true);
        iconPanel.SetActive(true);

    }

}
