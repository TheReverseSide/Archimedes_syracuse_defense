using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResistGlial : MonoBehaviour
{

    float wallHealth;
    float startHealth = 100f;
    bool isDestroyed;
    public int wallCellCount;


    public GameObject wallCellPrefab;

    public GameObject centerPoint;
    public Vector3 size;

    void Start()
    {
        wallHealth = 100; //Full health when placed

        CreateWall();

        //Start building wall animation

    }

    private void FixedUpdate()
    {
        if (wallHealth <= 0)
        {
            StartCoroutine("WallDestroyed()");
        }
    }

    public void TakeDamage(float amount)
    {
        wallHealth -= amount;

        //Should delete or remove some of the wall cells

        if (wallHealth <= 0 && isDestroyed == false)
        { StartCoroutine("WallDestroyed()"); }
    }

    public void CreateWall()
    { //Spawns wall of cells that acts as another barrier, ideal to put a splash damage turret on this glial

        for (int i = 0; i < wallCellCount; i++)
        {
            // I have to make the spawning in a rough area, not in the exact same spot
            //Or, I could spawn them in the same spot and have the animation move them into place
            SpawnCell();

        }
    }

    public void SpawnCell()
    {
        Vector3 pos = centerPoint.transform.position + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2)); //Dividing by two because this is the middle point

        Instantiate(wallCellPrefab, pos, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, .5f);
        //Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawCube(centerPoint.transform.position, size);
    }

    public IEnumerator WallDestroyed()
    {
        //Remove old wall
        isDestroyed = true;

        yield return new WaitForSeconds(5f);

        //Call anim to build wall

        CreateWall();
    }
}
