using UnityEngine;

public class Waypoints : MonoBehaviour
{
    //use Transform for GameObjects in the scene - in this case a list
    public static Transform[] waypoints; //static so we dont need a reference to the script - static is globally accessible

    private void Awake()
    {
        waypoints = new Transform[transform.childCount]; //founds the amount of children in the referenced list (our waypoints game object)	

        for (int i = 0; i < waypoints.Length; i++)//iterating through and adding found children to array
        {
            waypoints[i] = transform.GetChild(i);
        }
    }
}
