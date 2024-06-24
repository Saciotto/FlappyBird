using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    public float Speed = 0;
    public GameObject SpawnPosition;
    public GameObject DestroyPosition;

    private void Start()
    {
        if (transform.position != SpawnPosition.transform.position) {
            Instantiate(gameObject, SpawnPosition.transform.position, Quaternion.identity);
        }
    }

    void FixedUpdate()
    {
        MoveBase();
        TestResetPosition();
    }

    void MoveBase()
    {
        transform.position = Vector3.MoveTowards(transform.position, DestroyPosition.transform.position, Speed * Time.deltaTime);
    }

    void TestResetPosition()
    {
        if (transform.position == DestroyPosition.transform.position) {
            transform.position = SpawnPosition.transform.position;
        }
    }
}
