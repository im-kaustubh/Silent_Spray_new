using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;               // Assign the player in the Inspector
    public Vector3 offset = new Vector3(0, 0, -10);
    public float smoothSpeed = 5f;         // Higher = faster follow

    void LateUpdate()
    {
        Vector3 targetPosition = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
