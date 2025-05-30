using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // Speed of the player movement
    [SerializeField] private float jumpForce = 5f; // Force applied when the player jumps
    [SerializeField] private LayerMask groundLayer; // Layer mask for the ground layer
    [SerializeField] private Transform groundCheck; // Transform to check if the player is grounded
    private bool isGrounded; // Flag to check if the player is grounded
    private Animator animator;
    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    private GameManager gameManager;
    private AudioManager audioManager;
    private bool isAttacking = false;
    private bool isHit = false;
    private bool isDead = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindAnyObjectByType<GameManager>();
        audioManager = FindAnyObjectByType<AudioManager>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameManager.IsGameOver() || gameManager.IsGameWin())
            return;
        HandleMovement();
    }
    void Update()
    {
        if (gameManager.IsGameOver() || gameManager.IsGameWin()) // Check if the game is over
        {
            return; // Exit the Update method if the game is over
        }
        if (Input.GetMouseButtonDown(0) && !isAttacking && !isHit && !isDead)
        {
            Attack();
        }
        HandleJump(); // Call the HandleJump method to check for jump input
        UpdateAnimation(); // Call the UpdateAnimation method to update the animation state
    }
    private void HandleMovement()
    {
        // Get input from the horizontal and vertical axes (WASD or arrow keys)
        float moveX = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveX * speed, rb.linearVelocity.y); // Set the horizontal velocity based on input and speed
        if (moveX != 0)
        {
            // Flip the player sprite based on the direction of movement
            transform.localScale = new Vector3(Mathf.Sign(moveX), 1, 1);
        }
    }

    private void HandleJump()
    {
        // Check if the player is grounded by using a raycast
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        if (isGrounded && Input.GetButtonDown("Jump")) // Check if the player is grounded and the jump button is pressed
        {
            audioManager.PlayJumpSound();
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
        }

    }
    private void UpdateAnimation()
    {
        bool isRunning = Mathf.Abs(rb.linearVelocity.x) > 0.1f; // Check if the player is moving
        animator.SetBool("isRunning", isRunning); // Set the "isRunning" parameter in the Animator based on movement

        if (isGrounded && rb.linearVelocity.y <= 0) // Check if the player is grounded and not jumping
        {
            animator.SetBool("isJumping", false); // Set the "isJumping" parameter in the Animator to false when grounded
        }
    }
    public void Attack()
    {
        if (isDead || isHit) return;
        isAttacking = true;
        animator.SetBool("Attack", true);
    }
    public void Hit()
    {
        if (isDead) return;
        isHit = true;
        animator.SetTrigger("Hit");
    }
    public void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");
        rb.linearVelocity = Vector2.zero;
    }
    public void EndAttack()
    {
        isAttacking = false;
        animator.SetBool("Attack", false);
    }
    public void EndHit()
    {
        isHit = false;
    }
    
}
