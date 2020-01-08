using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class Wave
{
    public string waveName; //wave name
    public float spawnRate; //spawn rate 

    public List<Enemy> enemyList = new List<Enemy>(); //THis just sucks because you haev to enter them one by one

    public Dictionary<string, int> formEnemyTypeAndCount()
    {
        int simpleEnemyCount = 0;
        int fastEnemyCount = 0;
        int toughEnemyCount = 0;
        int swarmEnemyCount = 0;

        foreach (var enemy in enemyList)
        {
            if (enemy.name.Equals("Enemy_Fast") || enemy.name.Equals("Enemy_Fast(Clone)") || enemy.name.Equals("Enemy_Fast(clone)"))
            {
                fastEnemyCount++;
            }
            else if (enemy.name.Equals("Enemy_Simple") || enemy.name.Equals("Enemy_Simple(Clone)") || enemy.name.Equals("Enemy_Simple(clone)"))
            {
                simpleEnemyCount++;
            }
            else if (enemy.name.Equals("Enemy_Tough") || enemy.name.Equals("Enemy_Tough(Clone)") || enemy.name.Equals("Enemy_Tough(clone)"))
            {
                toughEnemyCount++;
            }
            else if (enemy.name.Equals("Swarm_Enemy") || enemy.name.Equals("Swarm_Enemy(Clone)") || enemy.name.Equals("Swarm_Enemy(clone)"))
            {
                swarmEnemyCount++;
            }
        }
        Dictionary<string, int> enemyReadout = new Dictionary<string, int>();

        enemyReadout.Add("Simple Enemy", simpleEnemyCount);
        enemyReadout.Add("Fast Enemy", fastEnemyCount);
        enemyReadout.Add("Tough Enemy", toughEnemyCount);
        enemyReadout.Add("Swarm Enemy", swarmEnemyCount);

        return enemyReadout;
    }

    //[Header("Wave count enemies")]
    //public int enemyOneCount;
    //public int enemyTwoCount;
    //public int enemyThreeCount;
    //public int enemyFourCount;


    //[Header("Enemy GameObjects")]
    //public GameObject enemy1;
    //public GameObject enemy2;
    //public GameObject enemy3;
    //public GameObject enemy4;
}
