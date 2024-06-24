using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Impulse = 5;
    public float MaxSpeed = 5;

    bool impulseTriggered = false;
    bool dead = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) {
            impulseTriggered = true;
        }
    }

    void FixedUpdate()
    {
        if (impulseTriggered && !dead) {
            ImpulsePlayer();
        }
    }

    void ImpulsePlayer()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, 0);
        rb.AddForce(Vector2.up * Impulse, ForceMode2D.Impulse);
        impulseTriggered = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle") {
            dead = true;
        }
    }

}
