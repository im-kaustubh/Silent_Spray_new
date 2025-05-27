using UnityEngine;

public class DroneMovement : MonoBehaviour
{
    public float speed = 2f;
    public float patrolDistance = 4f;

    private Vector3 startPosition;
    private int direction = 1;
    private float originalXScale;

    void Start()
    {
        startPosition = transform.position;
        originalXScale = Mathf.Abs(transform.localScale.x);  // Store the custom size
    }

    void Update()
    {
        // Move the drone
        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);

        // Flip based on direction, but preserve original scale
        Vector3 scale = transform.localScale;
        scale.x = direction > 0 ? originalXScale : -originalXScale;
        transform.localScale = scale;

        // Reverse direction at patrol limits
        if (transform.position.x > startPosition.x + patrolDistance)
        {
            direction = -1;
        }
        else if (transform.position.x < startPosition.x - patrolDistance)
        {
            direction = 1;
        }
    }
}
