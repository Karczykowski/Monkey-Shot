using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : MonoBehaviour
{
    public float speed = 5f;
    public Transform square;
    //Vector2 direction;
    public bool canEnemyMove;
    public List<Transform> waypoints;
    public EnemySpawner originSpawner;

    public MovementEnum MovementType;

    public Transform rotationCenter;
    public float rotationRadius;
    public float angularSpeed;
    public float xDivider;
    public float yDivider;
    public int freezeIndex;
    public bool isFreezing;
    public float howLongToFreeze;
    public GameObject monkeyHitEffect;
    public bool useVine;
    public LineRenderer lineRenderer;
    public Transform vinePoint;
    public Sprite sprite;
    SpriteRenderer rend;

    private ComboSystem _comboSystem;
    private GameManager _gameManager;
    void Start()
    {
        //direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rend = GetComponent<SpriteRenderer>();
        _gameManager = GameManager.instance;
        _comboSystem = GameObject.Find("GameManager").GetComponent<ComboSystem>();
        rend.sprite = sprite;
        gameObject.AddComponent<BoxCollider2D>();
    }

    void Update()
    {
        if(useVine)
        {
            LockOnTarget();

            lineRenderer.enabled = true;

            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, vinePoint.position);
        }

        /*if (!canEnemyMove)
            return;

        if (Mathf.Abs(transform.position.x) >= (square.localScale.x / 2) - 1)
        {
            direction = new Vector2(-direction.x, direction.y);
        }

        if (Mathf.Abs(transform.position.y) >= (square.localScale.y / 2) - 1)
        {
            direction = new Vector2(direction.x, -direction.y);
        }

        transform.Translate(direction.normalized * speed * Time.deltaTime);
        */

    }

    public void KillMonkey()
    {
        int number = Random.Range(1, 5);
        AudioManager.instance.Play("Monkey Scream" + number.ToString());
        Instantiate(monkeyHitEffect, transform.position, Quaternion.identity);
        _gameManager.IncreasePoints(1 * _comboSystem.scoreMultiplier);
        SpawnManager.currentMonkeyCount--;
        originSpawner.isTaken = false;
        _comboSystem.ScorePoints(1);
        Destroy(gameObject);
    }

    void LockOnTarget()
    {
        Vector3 dir = vinePoint.position - transform.position;
        Vector3 rotatedVectorDir = Quaternion.Euler(0, 0, 0) * dir;
        Quaternion lookRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorDir);
        transform.rotation = lookRotation;
    }
}
