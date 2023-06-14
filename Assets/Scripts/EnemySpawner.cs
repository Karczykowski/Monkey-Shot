using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Monkey;

    public Sprite sprite;
    //public Transform square;
    [HideInInspector]
    public List<Transform> points;

    public GameObject monkeySpawnEffect;

    [HideInInspector]
    public bool isTaken;

    [HideInInspector]
    public MovementEnum movementType;

    [HideInInspector]
    public Transform rotationCenter;

    [HideInInspector]
    public float rotationRadius = 2f, angularSpeed = 2f;

    [HideInInspector]
    public float xDivider, yDivider = 1f;

    [HideInInspector]
    public bool isFreezing;

    [HideInInspector]
    public int freezeIndex;

    [HideInInspector]
    public float howLongToFreeze;

    [HideInInspector]
    public bool useVine;

    [HideInInspector]
    public Transform vinePoint;


    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            points.Add(transform.GetChild(i));
        }
        isTaken = false;
    }

    public void SpawnMonkeyOnPosition()

    {
        Instantiate(monkeySpawnEffect, transform.position, new Quaternion(-1, Quaternion.identity.y, Quaternion.identity.z, 1));
        Monkey tempMonkey = Instantiate(Monkey, transform.position, Quaternion.identity).GetComponent<Monkey>();
        tempMonkey.sprite = sprite;
        //tempMonkey.square = square;
        tempMonkey.waypoints = points;
        tempMonkey.originSpawner = this;
        tempMonkey.MovementType = movementType;
        tempMonkey.rotationCenter = rotationCenter;
        tempMonkey.rotationRadius = rotationRadius;
        tempMonkey.angularSpeed = angularSpeed;
        tempMonkey.xDivider = xDivider;
        tempMonkey.yDivider = yDivider;
        tempMonkey.isFreezing = isFreezing;
        tempMonkey.freezeIndex = freezeIndex;
        tempMonkey.howLongToFreeze = howLongToFreeze;
        tempMonkey.useVine = useVine;
        tempMonkey.vinePoint = vinePoint;
        isTaken = true;
    }
    /*public void SpawnEnemyInRandomLocation()
    {
        float x_border = Random.Range((-square.localScale.x/2)+1, (square.localScale.x/2)-1);
        float y_border = Random.Range((-square.localScale.y/2)+1, (square.localScale.y/2)-1);
        Monkey tempMonkey = Instantiate(Monkey, new Vector2(x_border, y_border), Quaternion.identity).GetComponent<Monkey>();
        tempMonkey.square = square;
    }
    */
}

