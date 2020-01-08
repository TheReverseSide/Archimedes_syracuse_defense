using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DropDownFireMode : MonoBehaviour
{
    List<string> modes = new List<string>() { "Select mode", "StayLocked", "Freefire" };

    public Dropdown dropdown;
    //public Text selectedOption;

    public Turret turret;
    public NodeUI tNodeUI;
    public Node tNode;

    void Start()
    {
        PopulateList();
        this.tNodeUI = this.GetComponent<NodeUI>();
    }

    public void DropDown_IndexChanged(int index)
    {

        this.tNode = tNodeUI.getNode();
        this.turret = tNode.ReturnTurret();
        tNodeUI.Hide();

        //turret = tNodeUI.nodeUItarget.GetComponent<Turret>();
        //Debug.Log(turret);

        if (index == 0)
        {
            return;
        }
        else if (index == 1)
        {
            turret.EngageStayLock();
        }
        else if (index == 2)
        {
            turret.EngageFreeLock();
        }
    }

    void PopulateList()
    {
        dropdown.AddOptions(modes);
    }
}
