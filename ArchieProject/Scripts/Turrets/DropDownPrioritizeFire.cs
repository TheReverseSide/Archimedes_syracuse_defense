using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DropDownPrioritizeFire : MonoBehaviour
{

    List<string> priority = new List<string>() { "Select enemy", "EnemyFast", "EnemyTough", "SwarmEnemy", "EnemySimple" };
    //Expand for most value, toughest, has AoE, etc? Could have a simple list stored here 

    public Dropdown dropdown;
    public string enemyTag;

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
        enemyTag = priority[index];
        this.tNode = tNodeUI.getNode();
        this.turret = tNode.ReturnTurret();
        tNodeUI.Hide();

        //turret = tNodeUI.nodeUItarget.GetComponent<Turret>();
        //Debug.Log(turret);

        if (index == 0)
        {
            return;
        }
        else
        {
            Debug.Log("sending to prioritizer");
            turret.PrioritizeFire(enemyTag);
        }
    }

    void PopulateList()
    {
        dropdown.AddOptions(priority);
    }
}
