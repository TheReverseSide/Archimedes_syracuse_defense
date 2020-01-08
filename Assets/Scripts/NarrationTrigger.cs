using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NarrationTrigger : MonoBehaviour
{
    public Narration narration;
    public Button thisButton;
    public Button contButton;

    void Start()
    {
        contButton.gameObject.SetActive(false);
        contButton.interactable = false;
    }

    public void TriggerNarration()
    {
        FindObjectOfType<NarrationManager>().StartNarration(narration);

        this.gameObject.SetActive(false);
        thisButton.interactable = false;

        contButton.gameObject.SetActive(true);
        contButton.interactable = true;
    }
}
