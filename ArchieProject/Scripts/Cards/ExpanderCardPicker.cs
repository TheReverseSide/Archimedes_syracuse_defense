using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ExpanderCardPicker : MonoBehaviour
{ //WHen expander is selected, it picks the card associated with the icon and spawns it

    public Sprite cardImage; //If I use an image can it still be draggable?
    public BodyExpander bodyExpander;

    public void SelectAndSet()
    {
        bodyExpander.GetComponent<SpriteRenderer>().sprite = cardImage;
    }
}
