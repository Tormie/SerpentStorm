using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeMovement : MonoBehaviour
{
    TickManager tm;
    public int desiredDirection = 0; //0 = forward, 1 = right, 2 = left;
    [SerializeField]
    Image directionArrow;
    [SerializeField]
    GameObject bodyModulePrefab;
    public List<GameObject> bodyParts;
    Vector3 moduleSpawnOffSet = Vector3.back;
    int bodyLength = 1;
    public Vector3 preMovePosition;
    public Quaternion preMoveRotation;
    // Start is called before the first frame update
    void Start()
    {
        bodyParts = new List<GameObject>();
        tm = GameObject.Find("GameEngine").GetComponent<TickManager>();
        preMovePosition = transform.position;
        preMoveRotation = transform.rotation;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            desiredDirection = 2;
            directionArrow.transform.localRotation = Quaternion.Euler(0, 0, 90);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            desiredDirection = 1;
            directionArrow.transform.localRotation = Quaternion.Euler(0, 0, -90);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            desiredDirection = 2;
            directionArrow.transform.localRotation = Quaternion.Euler(0, 0, 90);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            desiredDirection = 1;
            directionArrow.transform.localRotation = Quaternion.Euler(0, 0, -90);
        }
    }

    public void ReceiveTick()
    {
        if (desiredDirection == 1)
        {
            transform.Rotate(new Vector3(0, 90, 0));
            desiredDirection = 0;
            directionArrow.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (desiredDirection == 2)
        {
            transform.Rotate(new Vector3(0, -90, 0));
            desiredDirection = 0;
            directionArrow.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        //MoveForward();
        //FireWeapons();
        StartCoroutine(RunLogic());
    }

    public IEnumerator RunLogic()
    {
        MoveForward();
        FireWeapons();
        yield return null;
    }

    public void FireWeapons()
    {
        GetComponent<TurretBehavior>().GetTarget();
    }

    public void MoveForward()
    {
        preMovePosition = transform.position;
        preMoveRotation = transform.rotation;
        transform.Translate(Vector3.forward);
        MoveParts();
    }

    void MoveParts()
    {
        if (bodyParts.Count == 0)
        {
            return;
        }

        foreach(GameObject g in bodyParts)
        {
            g.GetComponent<SnakeBodyPartMover>().NewMovePart();
        }

        /*Vector3 oldPartPosition = bodyParts[0].transform.position;
        Quaternion oldPartRotation = bodyParts[0].transform.rotation;
        bodyParts[0].transform.position = preMovePosition;
        bodyParts[0].transform.rotation = preMoveRotation;
        if(bodyParts.Count > 1)
        {
            bodyParts[1].GetComponent<SnakeBodyPartMover>().MovePart(oldPartPosition, oldPartRotation, 1);
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SnakeProtoPickup"))
        {
            Destroy(other.gameObject);
            if (bodyParts.Count == 0)
            {
                bodyParts.Add(Instantiate(bodyModulePrefab, transform.position - transform.forward, transform.rotation));
            }
            else
            {
                bodyParts.Add(Instantiate(bodyModulePrefab, bodyParts[bodyParts.Count-1].transform.position - bodyParts[bodyParts.Count - 1].transform.forward, bodyParts[bodyParts.Count - 1].transform.rotation));
            }
        }
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject g = bodyParts[bodyParts.Count - 1];
            bodyParts.RemoveAt(bodyParts.Count-1);
            Destroy(g);
        }
    }*/
}
