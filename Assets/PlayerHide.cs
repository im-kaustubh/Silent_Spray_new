using UnityEngine;

public class PlayerHide : MonoBehaviour
{
    public GameObject hidePrompt; //  HidePromptText here in Inspector
    private HidePromptFader promptFader;

    private bool canHide = false;
    public bool isHiding = false;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (hidePrompt != null)
        {
            promptFader = hidePrompt.GetComponent<HidePromptFader>();
            promptFader.Hide(); // start invisible
            hidePrompt.SetActive(true);
        }
    }

    void Update()
    {
        // Press H to hide
        if (canHide && !isHiding && Input.GetKeyDown(KeyCode.H))
        {
            
            isHiding = true;
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f); // semi-transparent
            Debug.Log("🔒 Player is hiding");

            if (promptFader != null)
                promptFader.Hide(); // hide prompt

        }

        // If hiding and player moves, unhide
        if (isHiding && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0))
        {
            isHiding = false;
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f); // fully visible
            Debug.Log("🚶‍♂️ Player moved and is now visible");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HidingSpot"))
        {
            canHide = true;

            if (!isHiding && promptFader != null)
                promptFader.Show(); // fade in
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
                spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
                Debug.Log("🚶‍♂️ Left hiding spot, unhidden");
            }

            if (promptFader != null)
                promptFader.Hide(); // fade out
        }
    }
}
