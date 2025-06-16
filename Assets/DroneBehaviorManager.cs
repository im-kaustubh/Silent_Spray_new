using UnityEngine;

public enum DroneState
{
    Patrol,
    Spotlight,
    Alert
}

public class DroneBehaviorManager : MonoBehaviour
{
    public DroneState currentState = DroneState.Patrol;

    public Sprite patrolSprite;
    public Sprite spotlightSprite;
    public Sprite alertSprite;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateDroneAppearance();
    }

    public void SetState(DroneState newState)
    {
        currentState = newState;
        UpdateDroneAppearance();
    }

    void UpdateDroneAppearance()
    {
        switch (currentState)
        {
            case DroneState.Patrol:
                spriteRenderer.sprite = patrolSprite;
                break;
            case DroneState.Spotlight:
                spriteRenderer.sprite = spotlightSprite;
                break;
            case DroneState.Alert:
                spriteRenderer.sprite = alertSprite;
                break;
        }
    }
}
