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
    private Animator animator;
    private Rigidbody2D body;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

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
            body.velocity = Vector2.zero;
            body.AddForce(Vector2.up * Impulse, ForceMode2D.Impulse);
            impulsePressed = false;
        }

        if (GameEvents.Instance.CurrentState != GameState.GameOver)
        {
            animator.SetFloat("Speed", body.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
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
