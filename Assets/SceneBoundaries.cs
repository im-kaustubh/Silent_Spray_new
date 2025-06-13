using UnityEngine;

public class SceneBoundary : MonoBehaviour
{
    public Transform leftBoundary;
    public Transform rightBoundary;

    public float MinX => leftBoundary.position.x;
    public float MaxX => rightBoundary.position.x;
}
