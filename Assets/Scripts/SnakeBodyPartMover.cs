using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBodyPartMover : MonoBehaviour
{
    SnakeMovement sm;
    public Vector3 oldPartPosition;
    public Quaternion oldPartRotation;
    // Start is called before the first frame update
    void Start()
    {
        sm = GameObject.FindGameObjectWithTag("Player").GetComponent<SnakeMovement>();
    }
    public void MovePart(Vector3 movePos, Quaternion moveRot, int partIndex)
    {
        oldPartPosition = transform.position;
        oldPartRotation = transform.rotation;
        transform.position = movePos;
        transform.rotation = moveRot;
        if (sm.bodyParts.Count > partIndex+1)
        {
            sm.bodyParts[1].GetComponent<SnakeBodyPartMover>().MovePart(oldPartPosition, oldPartRotation, partIndex+1);
        }
    }

    public void NewMovePart()
    {
        oldPartPosition = transform.position;
        oldPartRotation = transform.rotation;
        int i = sm.bodyParts.IndexOf(gameObject);
        switch (i)
        {
            case 0:
                transform.position = sm.preMovePosition;
                transform.rotation = sm.preMoveRotation;
                break;
            default:
                transform.position = sm.bodyParts[i-1].GetComponent<SnakeBodyPartMover>().oldPartPosition;
                transform.rotation = sm.bodyParts[i - 1].GetComponent<SnakeBodyPartMover>().oldPartRotation;
                break;
        }
        FireWeapons();
    }

    public void FireWeapons()
    {
        GetComponent<TurretBehaviorRandomTarget>().GetTarget();
    }
}
