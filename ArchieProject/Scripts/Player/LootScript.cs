using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class LootScript : MonoBehaviour
{
    System.Random random = new System.Random();

    [SerializeField]
    public List<CIED> lootList = new List<CIED>();

    public List<GameObject> rewardedLootList = new List<GameObject>();

    public int dropChance;

    public void CalculateLoot()
    {
        int calculatedDropChance = random.Next(0, 101); //compares against dropChance

        if (calculatedDropChance > dropChance)
        {
            Debug.Log("ths time no loot");
            return;
        }
        else //Half the time we get loot
        {
            int itemWeight = 0;

            for (int i = 0; i < lootList.Count; i++) //Adds all weights (rarities) from objects
            {
                itemWeight += lootList[i].dropRarity;
            }
            //Debug.Log("item weight: " + itemWeight);

            int randomNumFromWeight = random.Next(0, itemWeight);

            for (int j = 0; j < lootList.Count; j++)
            {
                if (randomNumFromWeight <= lootList[j].dropRarity)
                { //compares rarity (drop rarity) of all objects in list
                    PlayerStats.cardDeck.Add(lootList[j].gameObject); //O_ I dont think it can find the game objject that it needs to spawn

                    //I should also add them to the level won screen, or whereever I am going to palce them (would be cool to plcae cards instead of CIEDs)

                    rewardedLootList.Add(lootList[j].gameObject); //Add to list to present in the level completed screen

                    Debug.Log(lootList[j].gameObject + "earned");
                    return; //Prevents us form continuing the loop once we get loop
                }
                //if we havent gotten loot, decrement random value
                randomNumFromWeight -= lootList[j].dropRarity;
                //Debug.Log("random val decreased: " + randomNumFromWeight);
            }
        }
    }

    public List<GameObject> sendBackRewards()
    {
        return rewardedLootList;
    }
}


