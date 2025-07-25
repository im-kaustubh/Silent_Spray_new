using UnityEngine;

public class DroneMovement : MonoBehaviour
{
    public float speed = 2f;                   // Normal patrol speed
    public float patrolDistance = 4f;          // Distance it patrols

    private float currentSpeed;                // Can be boosted temporarily
    private Vector3 startPosition;
    private int direction = 1;
    private float originalXScale;

    void Start()
    {
        startPosition = transform.position;
        originalXScale = Mathf.Abs(transform.localScale.x);

        currentSpeed = speed;

        // Register to DroneManager
        if (DroneManager.Instance != null)
            DroneManager.Instance.RegisterDrone(this);
    }

    void Update()
    {
        // Move the drone
        transform.Translate(Vector3.right * direction * currentSpeed * Time.deltaTime);

        // Flip sprite
        Vector3 scale = transform.localScale;
        scale.x = direction > 0 ? originalXScale : -originalXScale;
        transform.localScale = scale;

        // Reverse direction at patrol bounds
        if (transform.position.x > startPosition.x + patrolDistance)
        {
            direction = -1;
        }
        else if (transform.position.x < startPosition.x - patrolDistance)
        {
            direction = 1;
        }
    }

    // Called by DroneManager
    public void SetSpeedBoost(float multiplier)
    {
        currentSpeed = speed * multiplier;
    }

    public void ResetSpeed()
    {
        currentSpeed = speed;
    }
}
