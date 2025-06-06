﻿/*
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
*/
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

            // Disable animation and switch to hiding sprite
            animator.enabled = false;
            spriteRenderer.sprite = hidingSprite;

            Debug.Log("🔒 Player is hiding");

            if (promptFader != null)
                promptFader.Hide();
        }

        if (isHiding && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0))
        {
            isHiding = false;

            // Re-enable animation and restore normal sprite
            animator.enabled = true;
            spriteRenderer.sprite = normalSprite;

            Debug.Log("🚶‍♂️ Player moved and is now visible");
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
                Debug.Log("🚶‍♂️ Left hiding spot, unhidden");
            }

            if (promptFader != null)
                promptFader.Hide();
        }
    }
}