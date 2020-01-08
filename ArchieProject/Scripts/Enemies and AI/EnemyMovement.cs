using UnityEngine;

[RequireComponent(typeof(Enemy))] //This class wont work unless we have this component
public class EnemyMovement : MonoBehaviour
{
    private Transform target; //private do not show up in inspector
    private int wavepointIndex = 0; //index number of the current waypoint

    private Enemy enemy; //initializing enemy object

    void Start()
    {
        target = Waypoints.waypoints[0]; //The reference above is what is allowed when you make something (the array) static\
        enemy = GetComponent<Enemy>(); //bringing in funciontaliyt from enemy class
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World); //enemy.speed since we are taking functionality from Enemy
                                                                                         //translate moves transform in direction and distance given by translation
                                                                                         //normalized regulates speed to 1 to the only thing that really controls speed is our speed variable
                                                                                         //adding time.deltatime makes sure that the rate we move is dependent on time, not the frame rate - in a way it compensates because if alot of time has passed, it moves faster in accordance

        //if distance from waypoint is less than .2, we have arrived and move on to the next
        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        { GetNextWaypoint(); }

        enemy.speed = enemy.startSpeed; //This is to return the speed back to start speed when they are no longer being hit with a laser
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.waypoints.Length - 1)
        { Debug.Log("See script - need to implement method"); } //IMplement PathEnded();

        else
        {
            wavepointIndex++;
            target = Waypoints.waypoints[wavepointIndex];
        }
    }
}
