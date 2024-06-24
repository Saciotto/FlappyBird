using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float ObstacleSpawnInterval;
    public float ObstacleMinHeight;
    public float ObstacleMaxHeight;
    public GameObject Obstacle;
    public GameObject ObstacleSpawnPosition;
    public GameObject ObstacleDestroyPosition;

    float spawnTimeout = 0;

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
}
