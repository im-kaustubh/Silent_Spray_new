using UnityEngine;

public class CollisionDebugger : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("COLLISION DETECTED with: " + collision.gameObject.name);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("TRIGGER DETECTED with: " + other.gameObject.name);
    }
}