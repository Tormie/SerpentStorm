using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCore : MonoBehaviour
{
    public int killsPerLevel;
    public GameObject powerUp;
    int currentKills;
    int totalKills;

    public bool isPaused = false;

    public void AddKill()
    {
        currentKills++;
        totalKills++;
        if (currentKills >= killsPerLevel)
        {
            currentKills = 0;
            int varXPos = Random.Range(-9, 11);
            int varZPos = Random.Range(-9, 11);
            Vector3 spawnPosition = new Vector3(varXPos - 0.5f, 2, varZPos - 0.5f);
            Instantiate(powerUp, spawnPosition, transform.rotation);
        }
    }

}
