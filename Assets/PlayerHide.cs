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

            // Set transparency to 50%
            Color color = spriteRenderer.color;
            color.a = 0.7f;
            spriteRenderer.color = color;

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

            // Restore full opacity
            Color color = spriteRenderer.color;
            color.a = 1f;
            spriteRenderer.color = color;

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

                // Restore full opacity
                Color color = spriteRenderer.color;
                color.a = 1f;
                spriteRenderer.color = color;

                Debug.Log("Left hiding spot, unhidden");
            }

            if (promptFader != null)
                promptFader.Hide();
        }
    }
}
