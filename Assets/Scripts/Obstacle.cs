using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Obstacle : MonoBehaviour
{
    public float Speed;
    public GameObject DestroyPosition;
    public IObjectPool<GameObject> Pool;

    private void Start()
    {
        GameEvents.Instance.OnCurrentStateChanged += OnCurrentStateChanged;
    }

    private void FixedUpdate()
    {
        if (GameEvents.Instance.CurrentState != GameState.Alive)
        {
            return;
        }

        transform.position += Vector3.left * Speed * Time.deltaTime;

        if (transform.position.x < DestroyPosition.transform.position.x)
        {
            Pool.Release(gameObject);
        }
    }

    private void OnCurrentStateChanged(GameState state)
    {
        if (state != GameState.Alive)
        {
            foreach (BoxCollider2D boxCollider in GetComponentsInChildren<BoxCollider2D>())
            {
                boxCollider.enabled = false;
            }
        }
    }
}
