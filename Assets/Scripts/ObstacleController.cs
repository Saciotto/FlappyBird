using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public float Speed;
    public GameObject DestroyPosition;

    void FixedUpdate()
    {
        MoveObstacle();
        DestroyObstacleIfOutOfScene();
    }

    void MoveObstacle()
    {
        transform.position += Vector3.left * Speed * Time.deltaTime;
    }

    void DestroyObstacleIfOutOfScene()
    {
        if (transform.position.x <= DestroyPosition.transform.position.x) {
            Destroy(gameObject);
        }
    }
}
