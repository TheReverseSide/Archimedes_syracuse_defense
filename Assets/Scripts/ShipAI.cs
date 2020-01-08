using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class ShipAI : MonoBehaviour
{
    [Header("MOVEMENT AND NAV STUFF")]

    [HideInInspector]
    private Ship ship;
    public Transform closestNode;
    bool keepMoving;

    private void Awake() {
        ship = GetComponent<Ship>();
    }

    private void Start() {
        keepMoving = true;
        GetClosestNode();
    }

    private void Update() {
        //Improve logic to start shooting when in range
        // ship.range
        
        if (closestNode != null && keepMoving){
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, closestNode.transform.position, ship.speed);
        }
    }

    void GetClosestNode()
    {
        Transform[] nodes = FindObjectsOfType<Transform>();
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in nodes)
        {
            if (t.gameObject.CompareTag("Wall"))
            {
                float dist = Vector3.Distance(t.position, currentPos);
                if (dist < minDist)
                {
                    tMin = t;
                    minDist = dist;
                }
            }
        }
        closestNode = tMin;
        // print("Closest node" + closestNode.gameObject);
    }

    public void StopNav(){
        keepMoving = false;
    }
}