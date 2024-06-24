using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float ObstacleSpawnInterval;
    public float ObstacleMinHeight;
    public float ObstacleMaxHeight;
    public GameObject Obstacle;
    public GameObject ObstacleSpawnPosition;
    public GameObject ObstacleDestroyPosition;
    public bool IsGameOver = false;

    float spawnTimeout = 0;

    void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    void Update() {
        SpawnObstacle();
    }

    void SpawnObstacle() {
        spawnTimeout -= Time.deltaTime;
        if (spawnTimeout <= 0) {
            float height = Random.Range(ObstacleMinHeight, ObstacleMaxHeight);
            Vector2 spawnPosition = new Vector2(ObstacleSpawnPosition.transform.position.x, height);
            GameObject obstacle = Instantiate(Obstacle, spawnPosition, Quaternion.identity);
            obstacle.GetComponent<ObstacleController>().DestroyPosition = ObstacleDestroyPosition;
            spawnTimeout = ObstacleSpawnInterval;
        }
    }

    public void GameOver()
    {
        IsGameOver = true;
    }
}
