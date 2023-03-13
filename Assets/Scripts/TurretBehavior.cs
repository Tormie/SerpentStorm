using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurretBehavior : MonoBehaviour
{
    //Engine variables
    GameCore gc;
    AudioSource aus;

    //Common variables
    public float shotDamage = 5;
    public float fireRange = 2f;
    public GameObject bullet;
    GameObject tempTarget;
    public GameObject target = null;

    private void Start()
    {
        aus = GetComponent<AudioSource>();
        gc = GameObject.Find("GameEngine").GetComponent<GameCore>();
    }

    /*private void Update()
    {
        if (target == null)
        {
            GetTarget();
        }
        if (target)
        {
            if (Vector3.Distance(target.transform.position, transform.position) > fireRange)
            {
                target = null;
            }
        }
    }*/

    public void GetTarget()
    {
        Debug.Log("GetTarget run");
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, fireRange);
        float nearestDistance = Mathf.Infinity;
        foreach (Collider hc in hitColliders)
        {
            if (hc.gameObject.CompareTag("Enemy"))
            {
                float dist = Vector3.Distance(hc.gameObject.transform.position, transform.position);
                if (dist <= nearestDistance)
                {
                    nearestDistance = dist;
                    tempTarget = hc.gameObject;
                }
            }
            target = tempTarget;
        }
        if (target)
        {
            StartCoroutine(ShootCR());
        }
    }

    IEnumerator ShootCR()
    {
        if (target)
        {
            aus.Play();
            Vector3 lookVector = target.transform.position - transform.position;
            lookVector.y = 0;
            GameObject b = Instantiate(bullet, transform.position, Quaternion.LookRotation(lookVector));
            b.GetComponent<BulletBehavior>().damage = shotDamage;
        }
        yield return null;
    }
}
