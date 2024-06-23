using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Impulse = 5;
    public float MaxSpeed = 5;

    void Update()
    {
        // Impulse on mouse click or space key
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * Impulse, ForceMode2D.Impulse);
        }

        // Limit speed
        if (GetComponent<Rigidbody2D>().velocity.y > MaxSpeed) {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, MaxSpeed);
        }
    }
}
