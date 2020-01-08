using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : MonoBehaviour
{
	
    public GameObject enemy;
	public float move_speed = 3.0f;
	AudioSource audioData;


    void Start()
    {
        audioData = GetComponent<AudioSource>();
		StartCoroutine(Aud());
    }

    IEnumerator Aud()
    {
        while (true){
			audioData.Play(0);
			yield return new WaitForSeconds(Random.Range(.9f, 1.3f));
			audioData.Play(0);
		}

    }

    void Update()
    {
        Vector3 dir = enemy.transform.position - transform.position;
        transform.Translate(dir.normalized * move_speed * Time.deltaTime, Space.World);
    }
}
