using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public float Speed;
    public GameObject SpawnPosition;
    public GameObject DestroyPosition;

    void FixedUpdate()
    {
        if (GameEvents.Instance.CurrentState != GameState.Alive)
        {
            return;
        }

        transform.position = new Vector2(transform.position.x - Speed * Time.deltaTime, transform.position.y);

        if (transform.position.x <= DestroyPosition.transform.position.x)
        {
            transform.position = SpawnPosition.transform.position;
        }
    }
}
