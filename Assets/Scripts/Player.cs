using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Impulse;

    public bool isAlive
    {
        get => GameEvents.Instance.CurrentState == GameState.Alive;
    }

    private bool impulsePressed = false;

    private void Update()
    {
        if (isAlive && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))) 
        {
            impulsePressed = true;
        }
    }

    private void FixedUpdate()
    {
        if (impulsePressed)
        {
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * Impulse, ForceMode2D.Impulse);
            impulsePressed = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Animator animator = GetComponent<Animator>();

        if (collision.gameObject.tag == "Obstacle")
        {
            GameEvents.Instance.SetCurrentState(GameState.Dead);
            animator.SetBool("IsAlive", false);
        }
        else if (collision.gameObject.tag == "Floor")
        {
            GameEvents.Instance.SetCurrentState(GameState.GameOver);
            animator.SetBool("IsAlive", false);
        }
    }
}
