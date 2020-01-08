using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NodeUI : MonoBehaviour
{
    public GameObject nUI; //The UI canvas containing upgrade, sell, prioritize buttons
    public GameObject ClickOffCanvas;
    //public Turret turret;
    public GameObject expandedUpgrades;
    public GameObject displayUpgradesButton;
    public GameObject hideUpgradeButton;

    [HideInInspector]
    public Color originalButtonColor;

    public Text sellAmount;
    public Text upgradeCost;

    public Button upgradeButton;
    public Button sellButton;

    [Header("Upgrade branch stuff")]
    public TextMeshProUGUI branch1Text;
    public Button b1_u1_button;
    public Button b1_u2_button;
    public Button b1_u3_button;

    public TextMeshProUGUI branch2Text;
    public Button b2_u1_button;
    public Button b2_u2_button;
    public Button b2_u3_button;

    public TextMeshProUGUI branch3Text;
    public Button b3_u1_button;
    public Button b3_u2_button;
    public Button b3_u3_button;


    //Keeps track of current target (the node for which we are displaying the UI)
    private Node node;
    public Turret turret;

    public void Start()
    {
        originalButtonColor = b1_u1_button.image.GetComponent<Image>().color;

        b1_u2_button.image.GetComponent<Image>().color = new Color32(61, 60, 55, 100);
        b1_u3_button.image.GetComponent<Image>().color = new Color32(61, 60, 55, 100);

        b2_u2_button.image.GetComponent<Image>().color = new Color32(61, 60, 55, 100);
        b2_u3_button.image.GetComponent<Image>().color = new Color32(61, 60, 55, 100);

        b3_u2_button.image.GetComponent<Image>().color = new Color32(61, 60, 55, 100);
        b3_u3_button.image.GetComponent<Image>().color = new Color32(61, 60, 55, 100);

    }

    public void SetTarget(Node _target)
    {

        this.node = _target;

        transform.position = node.getBuildPosition(); //Moves our UI to the build position by receiving node from build manager (who receives it from Node)

        //if (!node.isUpgradedOne) //Base turret
        //{
        //    //  upgradeCost.text = "$" + node.turretBlueprint.upgradeOneCost;
        //    upgradeButton.interactable = true;
        //}
        //else if (!node.isUpgradedTwo) //First upgraded turret
        //{
        //    //            upgradeCost.text = "$" + node.turretBlueprint.upgradeTwoCost;
        //    upgradeButton.interactable = true;
        //}
        //else
        //{
        //    upgradeCost.text = "Done";
        //    upgradeButton.interactable = false; //Stops you from clicking button once upgraded
        //}

        sellAmount.text = "50G";

        turret = node.turret.GetComponent<Turret>();

        //Go get titles of the upgrades for this turret
        branch1Text.text = node.turret.GetComponent<Turret>().branch1Title;
        branch2Text.text = node.turret.GetComponent<Turret>().branch2Title;
        branch3Text.text = node.turret.GetComponent<Turret>().branch3Title;

        //Go get the names of all of the upgrades for this turret and apply them to the upgrade menu
        //BRANCH ONE
        b1_u1_button.GetComponentInChildren<Text>().text = node.turret.GetComponent<Turret>().b1_u1_name;
        b1_u2_button.GetComponentInChildren<Text>().text = node.turret.GetComponent<Turret>().b1_u2_name;
        b1_u3_button.GetComponentInChildren<Text>().text = node.turret.GetComponent<Turret>().b1_u3_name;

        //BRANCH TWO
        b2_u1_button.GetComponentInChildren<Text>().text = node.turret.GetComponent<Turret>().b2_u1_name;
        b2_u2_button.GetComponentInChildren<Text>().text = node.turret.GetComponent<Turret>().b2_u2_name;
        b2_u3_button.GetComponentInChildren<Text>().text = node.turret.GetComponent<Turret>().b2_u3_name;

        //BRANCH THREE
        b3_u1_button.GetComponentInChildren<Text>().text = node.turret.GetComponent<Turret>().b3_u1_name;
        b3_u2_button.GetComponentInChildren<Text>().text = node.turret.GetComponent<Turret>().b3_u2_name;
        b3_u3_button.GetComponentInChildren<Text>().text = node.turret.GetComponent<Turret>().b3_u3_name;

        //Set UI as it needs to be
        nUI.SetActive(true);
        ClickOffCanvas.SetActive(true);
        displayUpgradesButton.SetActive(true);
        expandedUpgrades.SetActive(false);
        hideUpgradeButton.SetActive(false);
    }

    public void Hide()
    {
        nUI.SetActive(false);
        ClickOffCanvas.SetActive(false);
        displayUpgradesButton.SetActive(true);
        expandedUpgrades.SetActive(false);
        hideUpgradeButton.SetActive(false);
    }

    public void Upgrade()
    {
        this.Hide();

        //Here, check whatever branch has been selected, then disable all other branches

        // node.UpgradeTurret();
        BuildManager.instance.DeselectNode(); //we deselect instead of hide, because then the UI would just be hidden, yet still selected
    }

    public void Sell()
    {
        this.Hide();

        node.SellTurret();
        PlayerStats.currentGlucose += 50;
        AudioManager.instance.Play("TurretSell");
        BuildManager.instance.DeselectNode();
    }

    public Node getNode()
    {
        return node;
    }

    #region BRANCH ONE
    public void B1_UI()
    {
        DisableBranch2();
        DisableBranch3();

        turret.B1_UI();

        b1_u1_button.enabled = false;
        b1_u1_button.image.GetComponent<Image>().color = new Color32(61, 60, 55, 100);

        b1_u2_button.image.GetComponent<Image>().color = originalButtonColor;
        b1_u3_button.image.GetComponent<Image>().color = originalButtonColor;

        BuildManager.instance.DeselectNode();
    }

    public void B1_U2()
    {
        turret.B1_U2();

        b1_u2_button.enabled = false;
        b1_u2_button.image.GetComponent<Image>().color = new Color32(61, 60, 55, 100);

        BuildManager.instance.DeselectNode();
    }

    public void B1_U3()
    {
        turret.B1_U3();

        b1_u3_button.enabled = false;
        b1_u3_button.image.GetComponent<Image>().color = new Color32(61, 60, 55, 100);

        upgradeButton.enabled = false;
        upgradeButton.image.GetComponent<Image>().color = new Color32(61, 60, 55, 100);

        BuildManager.instance.DeselectNode();
    }
    #endregion

    #region BRANCH TWO
    public void B2_UI()
    {
        DisableBranch1();
        DisableBranch3();

        turret.B2_UI();

        b2_u1_button.enabled = false;
        b2_u1_button.image.GetComponent<Image>().color = new Color32(61, 60, 55, 100);

        b2_u2_button.image.GetComponent<Image>().color = originalButtonColor;
        b2_u3_button.image.GetComponent<Image>().color = originalButtonColor;

        BuildManager.instance.DeselectNode();
    }

    public void B2_U2()
    {
        turret.B2_U2();

        b2_u2_button.enabled = false;
        b2_u2_button.image.GetComponent<Image>().color = new Color32(61, 60, 55, 100);

        BuildManager.instance.DeselectNode();
    }

    public void B2_U3()
    {
        turret.B2_U3();

        b2_u3_button.enabled = false;
        b2_u3_button.image.GetComponent<Image>().color = new Color32(61, 60, 55, 100);

        upgradeButton.enabled = false;
        upgradeButton.image.GetComponent<Image>().color = new Color32(61, 60, 55, 100);

        BuildManager.instance.DeselectNode();
    }
    #endregion

    #region BRANCH THREE
    public void B3_UI()
    {
        DisableBranch1();
        DisableBranch2();

        turret.B3_UI();

        b3_u1_button.enabled = false;
        b3_u1_button.image.GetComponent<Image>().color = new Color32(61, 60, 55, 100);

        b3_u2_button.image.GetComponent<Image>().color = originalButtonColor;
        b3_u3_button.image.GetComponent<Image>().color = originalButtonColor;

        BuildManager.instance.DeselectNode();
    }

    public void B3_U2()
    {
        turret.B3_U2();

        b3_u2_button.enabled = false;
        b3_u2_button.image.GetComponent<Image>().color = new Color32(61, 60, 55, 100);

        BuildManager.instance.DeselectNode();
    }

    public void B3_U3()
    {
        turret.B3_U3();

        b3_u3_button.enabled = false;
        b3_u3_button.image.GetComponent<Image>().color = new Color32(61, 60, 55, 100);

        upgradeButton.enabled = false;
        upgradeButton.image.GetComponent<Image>().color = new Color32(61, 60, 55, 100);

        BuildManager.instance.DeselectNode();
    }
    #endregion

    private void DisableBranch1()
    {
        b1_u1_button.enabled = false;
        b1_u1_button.image.GetComponent<Image>().color = new Color32(61, 60, 55, 50);

        b1_u2_button.enabled = false;
        b1_u2_button.image.GetComponent<Image>().color = new Color32(61, 60, 55, 50);

        b1_u3_button.enabled = false;
        b1_u3_button.image.GetComponent<Image>().color = new Color32(61, 60, 55, 50);
    }

    private void DisableBranch2()
    {
        b2_u1_button.enabled = false;
        b2_u1_button.image.GetComponent<Image>().color = new Color32(61, 60, 55, 50);

        b2_u2_button.enabled = false;
        b2_u2_button.image.GetComponent<Image>().color = new Color32(61, 60, 55, 50);

        b2_u3_button.enabled = false;
        b2_u3_button.image.GetComponent<Image>().color = new Color32(61, 60, 55, 50);
    }

    private void DisableBranch3()
    {

        b3_u1_button.enabled = false;
        b3_u1_button.image.GetComponent<Image>().color = new Color32(61, 60, 55, 50);

        b3_u2_button.enabled = false;
        b3_u2_button.image.GetComponent<Image>().color = new Color32(61, 60, 55, 50);

        b3_u3_button.enabled = false;
        b3_u3_button.image.GetComponent<Image>().color = new Color32(61, 60, 55, 50);
    }
}
