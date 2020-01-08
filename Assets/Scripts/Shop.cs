using UnityEngine;

public class Shop : MonoBehaviour {

	public TurretBlueprint mirrorTurret;
	public TurretBlueprint ballistaTurret;
	public TurretBlueprint forkTurret;

	BuildManager buildManager;

	void Start ()
	{
		buildManager = BuildManager.instance;
	}

	public void SelectMirrorTurret ()
	{
		// Debug.Log("Mirror Turret Selected");
		buildManager.SelectTurretToBuild(mirrorTurret);
	}

	public void SelectBallistaLauncher()
	{
		// Debug.Log("Ballista Selected");
		buildManager.SelectTurretToBuild(ballistaTurret);
	}

	public void SelectForkTurret()
	{
		// Debug.Log("Fork Selected");
		buildManager.SelectTurretToBuild(forkTurret);
	}

}
