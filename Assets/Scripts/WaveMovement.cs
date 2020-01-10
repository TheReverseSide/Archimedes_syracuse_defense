using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMovement : MonoBehaviour {

	Vector3 pos;
	public float direction = 1f;
	float frequency;
	float magnitude;

	// Use this for initialization
	void Start () {
		pos = transform.position;
		frequency = Random.Range (1.9f, 2.6f);
		magnitude = Random.Range (0.07f, 0.18f);
	}
	
	// Update is called once per frame
	void Update () {
		pos.z = Mathf.Sin (Time.time * frequency) * magnitude * direction;
		transform.position = pos;
	}
}
