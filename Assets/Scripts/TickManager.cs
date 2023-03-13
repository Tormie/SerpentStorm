using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickManager : MonoBehaviour
{
    public float tickTime = 1f;
    float tickLevelReduction = 0.05f;
    float tickTicker = 0;
    int tickCounter = 0;
    int tickSpeedLevel = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tickTicker += Time.deltaTime;
        if (tickTicker >= tickTime)
        {
            Tick();
            tickTicker = 0;
        }
    }

    void Tick()
    {
        tickCounter++;
        if (tickCounter == tickSpeedLevel * 25)
        {
            tickSpeedLevel++;
            tickTime -= tickLevelReduction;
            tickTime = Mathf.Clamp(tickTime, 0.1f, 1);
        }
        Debug.Log("Ticking");
        foreach(GameObject p in GameObject.FindGameObjectsWithTag("Player"))
        {
            p.GetComponent<SnakeMovement>().ReceiveTick();
        }
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            p.GetComponent<EnemyMovement>().ReceiveTick();
        }
        GetComponent<EnemySpawner>().ReceiveTick();
    }
}
