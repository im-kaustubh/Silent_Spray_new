using UnityEngine;

public class PlayerHide : MonoBehaviour
{
    public bool isHiding = false;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Trigger entered by: " + other.name + " with tag: " + other.tag);

        if (other.CompareTag("HidingSpot"))
        {
            isHiding = true;
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f); // semi-transparent
            Debug.Log("🔒 Player is hiding");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("HidingSpot"))
        {
            isHiding = false;
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f); // normal
            Debug.Log("🚶‍♂️ Player left hiding spot");
        }
    }
}
