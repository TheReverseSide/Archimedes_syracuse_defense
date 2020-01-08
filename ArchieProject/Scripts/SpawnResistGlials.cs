using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnResistGlials : MonoBehaviour
{

    public GameObject centerPoint;
    public Vector3 size;

    public GameObject cellWallPrefab;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnCell()
    {
        Vector3 pos = centerPoint.transform.position + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2)); //Dividing by two because this is the middle point

        Instantiate(cellWallPrefab, pos, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, .5f);
        Gizmos.DrawCube(centerPoint.transform.position, size);
    }
}
