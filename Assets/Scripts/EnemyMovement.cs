using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    GameObject player;
    Vector3 playerPosition;
    Vector3 playerOffSet;
    float playerXDistance;
    float playerZDistance;
    [SerializeField]
    int ticksPerMove = 1;
    int tickCounter = 0;
    public int moveDir = 0; //0 = up, 1 = right, 2 = down, 3 = left
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void ReceiveTick()
    {
        tickCounter++;
        if (tickCounter >= ticksPerMove)
        {
            Debug.Log("Enemy Tick Received");
            playerXDistance = transform.position.x - player.transform.position.x;
            playerZDistance = transform.position.z - player.transform.position.z;
            if (Mathf.Abs(playerXDistance) >= Mathf.Abs(playerZDistance)) // Xdistance is greater, so move horizontally
            {
                Debug.Log("Moving horizontally");
                if (player.transform.position.x > transform.position.x)
                {
                    moveDir = 1;
                }
                else
                {
                    moveDir = 3;
                }
            }
            else //Zdistance is greater, so move vertically
            {
                Debug.Log("Moving vertically");
                if (player.transform.position.z > transform.position.z)
                {
                    moveDir = 0;
                }
                else
                {
                    moveDir = 2;
                }
            }
            tickCounter = 0;
            MoveEnemy();
        }
    }

    void MoveEnemy()
    {
        switch (moveDir)
        {
            case 0:
                transform.Translate(0, 0, 1);
                break;
            case 1:
                transform.Translate(1, 0, 0);
                break;
            case 2:
                transform.Translate(0, 0, -1);
                break;
            case 3:
                transform.Translate(-1, 0, 0);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("PlayerPart"))
        {
            GameObject g = player.GetComponent<SnakeMovement>().bodyParts[player.GetComponent<SnakeMovement>().bodyParts.Count - 1];
            player.GetComponent<SnakeMovement>().bodyParts.RemoveAt(player.GetComponent<SnakeMovement>().bodyParts.Count - 1);
            Destroy(g);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("PlayerPart"))
        {
            GameObject g = player.GetComponent<SnakeMovement>().bodyParts[player.GetComponent<SnakeMovement>().bodyParts.Count - 1];
            player.GetComponent<SnakeMovement>().bodyParts.RemoveAt(player.GetComponent<SnakeMovement>().bodyParts.Count - 1);
            Destroy(g);
            Destroy(gameObject);
        }
    }
}
