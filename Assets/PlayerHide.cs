using UnityEngine;

public class PlayerHide : MonoBehaviour
{
    public GameObject hidePrompt;
    private HidePromptFader promptFader;

    public Sprite normalSprite;
    public Sprite hidingSprite;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private bool canHide = false;
    public bool isHiding = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        // Ensure player is on Ground sorting layer
        spriteRenderer.sortingLayerName = "Ground";
        spriteRenderer.sortingOrder = 1;

        if (hidePrompt != null)
        {
            promptFader = hidePrompt.GetComponent<HidePromptFader>();
            promptFader.Hide();
            hidePrompt.SetActive(true);
        }
    }

    void Update()
    {
        if (canHide && !isHiding && Input.GetKeyDown(KeyCode.H))
        {
            isHiding = true;

            // Disable animation & switch to hiding sprite
            animator.enabled = false;
            spriteRenderer.sprite = hidingSprite;

            // Move player behind hiding spot
            spriteRenderer.sortingOrder = -1;

            Debug.Log("🔒 Player is hiding");

            if (promptFader != null)
                promptFader.Hide();
        }

        if (isHiding && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0))
        {
            isHiding = false;

            // Enable animation & switch to normal sprite
            animator.enabled = true;
            spriteRenderer.sprite = normalSprite;

            // Restore sprite order to front
            spriteRenderer.sortingOrder = 1;

            Debug.Log("Player moved and is now visible");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HidingSpot"))
        {
            canHide = true;
            if (!isHiding && promptFader != null)
                promptFader.Show();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("HidingSpot"))
        {
            canHide = false;

            if (isHiding)
            {
                isHiding = false;
                animator.enabled = true;
                spriteRenderer.sprite = normalSprite;

                // Restore sprite order
                spriteRenderer.sortingOrder = 1;

                Debug.Log("Left hiding spot, unhidden");
            }

            if (promptFader != null)
                promptFader.Hide();
        }
    }
}
