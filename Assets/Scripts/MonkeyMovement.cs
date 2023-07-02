using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MonkeyMovement : MonoBehaviour
{
    private Transform target;

    Transform rotationCenter;
    float rotationRadius = 2f, angularSpeed = 2f;

    float posX, posY, angle = 0f;
    float xDivider, yDivider;

    private Monkey monkey;
    private int targetIndex = 0;
    private MovementEnum MovementType;

    bool goingRight = true;
    bool freeze = false;
    int freezeIndex;
    bool isFreezing;
    float howLongToFreeze;
    private void Start()
    {
        monkey = GetComponent<Monkey>();
        target = monkey.waypoints[targetIndex];
        MovementType = monkey.MovementType;
        rotationCenter = monkey.rotationCenter;
        rotationRadius = monkey.rotationRadius;
        angularSpeed = monkey.angularSpeed;
        xDivider = monkey.xDivider;
        yDivider = monkey.yDivider;
        freezeIndex = monkey.freezeIndex;
        isFreezing = monkey.isFreezing;
        howLongToFreeze = monkey.howLongToFreeze;
    }

    private void Update()
    {
        if (MovementType == MovementEnum.Backtracking)
            BacktrackMove();
        if (MovementType == MovementEnum.Linear)
            LinearMove();
        if (MovementType == MovementEnum.Circle)
            CircleMove();
    }



    void BacktrackMove()
    {
        if (monkey.transform.position == target.position)
        {
            if (targetIndex == freezeIndex && isFreezing)
            {
                freeze = true;
                StartCoroutine(waiter(howLongToFreeze));
            }
            if (goingRight)
            {
                if (targetIndex == monkey.waypoints.Count - 1)
                {
                    targetIndex = monkey.waypoints.Count - 2;
                    goingRight = false;
                }
                else
                {
                    targetIndex++;
                }
            }
            else
            {
                if (targetIndex == 0)
                {
                    targetIndex = 1;
                    goingRight = true;
                }
                else
                {
                    targetIndex--;
                }
            }
            target = monkey.waypoints[targetIndex];
        }
        if (freeze == false)
            transform.position = Vector2.MoveTowards(transform.position, target.position, monkey.speed * Time.deltaTime);
    }

    void LinearMove()
    {
        if (monkey.transform.position == target.position)
        {
            if (targetIndex == freezeIndex && isFreezing)
            {
                freeze = true;
                StartCoroutine(waiter(howLongToFreeze));
            }
            if (targetIndex == monkey.waypoints.Count - 1)
            {
                targetIndex = 0;
            }
            else
            {
                targetIndex++;
            }
            target = monkey.waypoints[targetIndex];
        }
        if(freeze==false)
            transform.position = Vector2.MoveTowards(transform.position, target.position, monkey.speed * Time.deltaTime);
    }

    void CircleMove()
    {
        posX = rotationCenter.position.x + Mathf.Cos(angle) * rotationRadius / xDivider;
        posY = rotationCenter.position.y + Mathf.Sin(angle) * rotationRadius / yDivider;
        transform.position = new Vector2(posX, posY);
        angle = angle + Time.deltaTime * angularSpeed * monkey.speed;

        if (angle >= 360f)
            angle = 0f;
    }

    IEnumerator waiter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        freeze = false;
    }
}
