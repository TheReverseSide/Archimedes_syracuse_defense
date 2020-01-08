using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretMenu : MonoBehaviour
{

    public GameObject registerCanvas;
    public GameObject loginCanvas;

    public void RegisterButton()
    {
        registerCanvas.SetActive(true);
        loginCanvas.SetActive(false);
    }

    public void LoginButton()
    {
        registerCanvas.SetActive(false);
        loginCanvas.SetActive(true);
    }
}
