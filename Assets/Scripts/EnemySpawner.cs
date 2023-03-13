using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    int side = 0; // 0 = top, 1 = right, 2 = bottom, 3 = left
    Vector3 spawnPosition;
    [SerializeField]
    GameObject enemyToSpawn;

    public void ReceiveTick()
    {
        side = Random.Range(0, 4);
        int varPos = Random.Range(-9, 11);
        float fVarPos = varPos-0.5f;
        switch (side)
        {
            case 0:
                spawnPosition = new Vector3(fVarPos, 2, 9.5f);
                break;
            case 1:
                spawnPosition = new Vector3(9.5f, 2, fVarPos);
                break;
            case 2:
                spawnPosition = new Vector3(fVarPos, 2, -9.5f);
                break;
            case 3:
                spawnPosition = new Vector3(-9.5f, 2, fVarPos);
                break;
        }
        Instantiate(enemyToSpawn, spawnPosition, transform.rotation);
    }
}
