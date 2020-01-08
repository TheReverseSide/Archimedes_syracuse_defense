using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolaArc : MonoBehaviour
{
    public float Animation; //Serves as a timer, starting at zero
    public float height;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Animation += Time.deltaTime;

        Animation = Animation % 5f; //Counts up to 5, then resets at 0, cycles like that

        transform.position = MathParabola.Parabola(Vector3.zero, Vector3.forward * 10, height, Animation / 5f); //Later sub: Vector3.zero with firePoint and Vector3.forward with enemy last known position
    }
}
