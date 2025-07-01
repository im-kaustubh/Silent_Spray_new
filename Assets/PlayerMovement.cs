using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;

    [SerializeField] private AudioSource walkingSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    void Update()
    {

        // Check if spraying
        bool isSpraying = Input.GetKey(KeyCode.E);
        animator.SetBool("isSpraying", isSpraying);

        // If spraying, disable movement
        if (isSpraying)
        {
            movement.x = 0f;
        }
        else
        {
            // Normal horizontal movement
            movement.x = Input.GetAxisRaw("Horizontal");
            //Debug.Log("movement.x: " + movement.x);

            // Flip sprite depending on direction
            if (movement.x != 0)
            {
                Vector3 scale = transform.localScale;
                scale.x = Mathf.Abs(scale.x) * (movement.x > 0 ? 1 : -1);
                transform.localScale = scale;

            }
        }

        // Set walking animation state
        animator.SetBool("isWalking", movement.x != 0);

        if (movement.x != 0 && !walkingSound.isPlaying)
        {
            walkingSound.Play();
        }
        else if (movement.x == 0 && walkingSound.isPlaying)
        {
            walkingSound.Stop();
        }

    }


    void FixedUpdate()
{
    if (movement.x == 0)
    {
        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
         
    }
    else
    {
        rb.linearVelocity = new Vector2(movement.x * moveSpeed, rb.linearVelocity.y);
           
    }
}
}
