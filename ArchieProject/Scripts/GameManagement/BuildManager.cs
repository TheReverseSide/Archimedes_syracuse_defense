using UnityEngine;

public class BuildManager : MonoBehaviour
{
    #region Variables
    public static BuildManager instance; //singleton is way to make sure that there is only one instance of build manager in the scene and static makes it easy to access

    [Header("Effects")]
    public GameObject buildEffect;
    public GameObject upgradeEffect;
    public GameObject sellEffect;
    public GameObject glucoseBurnEffect;

    [Header("Glials")]
    public GameObject slowGlialPrefab;

    public Vector3 slowGlialPosOffset = new Vector3(2.0f, 0, 0);

    [Header("UI")]
    public NodeUI nodeUI;
    public NodeShopUI nodeShopUI;

    public TurretBlueprint publicTurretList;

    private TurretBlueprint turretToBuild;
    private Node selectedTurretNode;

    //Add list of all glial cells here

    #endregion Variables

    #region# Properties
    public bool CanBuild { get { return turretToBuild != null; } } //a property, because it is only allowed to get something, can never be set

    public bool HasEnoughGlucose { get { return PlayerStats.currentGlucose >= turretToBuild.cannonBaseCost; } }
    #endregion Properties


    private void Awake()
    {
        if (instance != null) //to catch whether or not there is already a build manager in the scene
        {
            Debug.Log("More than one build manager");
            return;
        }
        instance = this; //Setting a reference to itself to be accessed from other classes
    }

    public void SelectNode(Node node)
    {
        /*if (selectedTurretNode == node) //if the incoming node is already equal to the selected node, deselects node
        {
            DeselectNode();
            return;
        }
        */

        selectedTurretNode = node;
        turretToBuild = null; //when we enable one, the other is disabled so we cannot select two at the same time

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedTurretNode = null;
        nodeUI.Hide();
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        DeselectNode(); //when we enable one, the other is disabled so we cannot select two at the same time
    }

    //since we dont we it visibile in the inspector (people could change it), we make a getter instead
    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }
}
