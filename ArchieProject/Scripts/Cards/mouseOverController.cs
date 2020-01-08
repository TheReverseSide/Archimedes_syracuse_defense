using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class mouseOverController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    float timeDur;
    bool counting;
    public Transform cardHand;
    public Transform zoomHand;

    void Start()
    {
        timeDur = 0;
        Vector3 startScale = this.GetComponent<RectTransform>().localScale;

        //float startPosX = this.GetComponent<RectTransform>().localPosition.x;
        //float startPosY = this.GetComponent<RectTransform>().localPosition.y;
        //float startPosZ = this.GetComponent<RectTransform>().localPosition.z;
    }

    void Update()
    {
        //Debug.Log(timeDur);
        if (timeDur >= 1.5f)
        {
            Instantiate(zoomHand, this.transform.position, Quaternion.identity);
            this.transform.SetParent(zoomHand);


            //this.GetComponent<RectTransform>().localPosition += new Vector3(0, -525, 0);
        }
        else
        {
            //this.GetComponent<RectTransform>().localScale = st
            this.transform.SetParent(cardHand);
            //if (zoomHand != null)
            //{
            //    
            //    Destroy(zoomHand);
            //}

            //this.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f); 
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        counting = true;
        StartCoroutine(startCounting());
    }

    IEnumerator startCounting()
    {
        while (counting)
        {
            timeDur += 1f;
            yield return new WaitForSeconds(1f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        counting = false;
        timeDur = 0;
    }
}
