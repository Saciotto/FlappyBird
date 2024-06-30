using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class Scenario : MonoBehaviour
{
    public static Scenario Instance { get; private set; }

    public float Speed;

    public float ObstacleSpawnInterval;
    public float ObstacleMinY;
    public float ObstacleMaxY;

    public GameObject ObstaclePrefab;
    public GameObject ObstacleSpawnPosition;
    public GameObject ObstacleDestroyPosition;

    public GameObject Base;
    public GameObject BaseSpawnPosition;
    public GameObject BaseDestroyPosition;

    private IObjectPool<GameObject> obstaclePool;
    private float obstacleSpawnTimeout;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            obstaclePool = new ObjectPool<GameObject>(CreateObstacle, OnGetObstacleFromPool, OnReleaseObstacleToPool, OnDestroyObstacle);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Floor floor = Base.GetComponent<Floor>();
        floor.Speed = Speed;
        floor.SpawnPosition = BaseSpawnPosition;
        floor.DestroyPosition = BaseDestroyPosition;

        GameObject nextBase = Instantiate(Base.gameObject);
        nextBase.transform.position = BaseSpawnPosition.transform.position;
        floor = nextBase.GetComponent<Floor>();
        floor.Speed = Speed;
        floor.SpawnPosition = BaseSpawnPosition;
        floor.DestroyPosition = BaseDestroyPosition;
    }

    private GameObject CreateObstacle()
    {
        GameObject obj = Instantiate(ObstaclePrefab);
        return obj;
    }

    private void OnReleaseObstacleToPool(GameObject obstacle)
    {
        obstacle.gameObject.SetActive(false);
    }

    private void OnGetObstacleFromPool(GameObject obstacle)
    {
        obstacle.gameObject.SetActive(true);
    }

    private void OnDestroyObstacle(GameObject obstacle)
    { 
        Destroy(obstacle);
    }

    private void FixedUpdate()
    {
        if (GameEvents.Instance.CurrentState != GameState.Alive)
        {
            return;
        }

        TestSpawnObstacle();
    }

    private void TestSpawnObstacle()
    {
        obstacleSpawnTimeout -= Time.deltaTime;
        if (obstacleSpawnTimeout <= 0)
        {
            GameObject obstacle = obstaclePool.Get();
            float y = UnityEngine.Random.Range(ObstacleMinY, ObstacleMaxY);
            obstacle.transform.position = new Vector2(ObstacleSpawnPosition.transform.position.x, y);
            obstacleSpawnTimeout = ObstacleSpawnInterval;

            Obstacle script = obstacle.GetComponent<Obstacle>();
            script.Speed = Speed;
            script.DestroyPosition = ObstacleDestroyPosition;
            script.Pool = obstaclePool;
        }
    }
}
