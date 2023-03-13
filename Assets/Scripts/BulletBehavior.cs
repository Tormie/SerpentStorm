using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float TTL = 5f;
    public float moveSpeed = 5f;
    public float damage = 5;
    public bool isHoming = false;
    public bool isEnemyShot = false;
    public GameObject target;
    public float rotateSpeed = 1.0f;
    GameCore gc;

    private void Start()
    {
        gc = GameObject.Find("GameEngine").GetComponent<GameCore>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!gc.isPaused)
        {
            TTL -= Time.deltaTime;
            if (isHoming)
            {
                if (target)
                {
                    Vector3 targetDirection = target.transform.position - transform.position;
                    Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotateSpeed * Time.deltaTime, 0.0f);
                    transform.rotation = Quaternion.LookRotation(newDirection);
                }

            }
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            if (TTL <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && !isEnemyShot)
        {
            gc.AddKill();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isEnemyShot)
        {
            gc.AddKill();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
