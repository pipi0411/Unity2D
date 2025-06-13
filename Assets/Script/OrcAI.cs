using UnityEngine;

public class OrcAI : MonoBehaviour
{
    [SerializeField] private BloodBarController bloodBar;
    public float speed = 2f;
    public Transform groundCheck;
    public Transform wallCheck;
    public LayerMask groundLayer;
    public float checkRadius = 0.2f;
    public float turnCooldown = 0.5f;
    public Transform player;
    public float detectRange = 5f;
    public float attackRange = 1.5f;
    public int attackDamage = 1;
    private float lastTurnTime = 0f;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isFacingRight = true;
    private bool isAttacking = false;
    private bool isDead = false;
    private bool isHit = false;
    private PlayerController playerController;
    private GameManager gameManager;
    private int maxHealth = 4;
    private int currentHealth;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gameManager = FindAnyObjectByType<GameManager>();

    }
    void Start()
    {
        currentHealth = maxHealth;
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
        }
    }

    void Update()
    {
        if (isDead)
        {
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("isWalking", false);
            return;
        }
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectRange)
        {
            if (distanceToPlayer <= attackRange)
            {
                AttackPlayer();
            }
            else
            {
                ChasePlayer();
            }
        }
        else
        {
            Patrol();
        }
        animator.SetBool("isWalking", Mathf.Abs(rb.linearVelocity.x) > 0.1f);
    }
    void Patrol()
    {
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        bool isWall = Physics2D.OverlapCircle(wallCheck.position, checkRadius, groundLayer);
        if ((!isGrounded || isWall) && Time.time > lastTurnTime + turnCooldown)
        {
            Flip();
            lastTurnTime = Time.time;
        }

        rb.linearVelocity = new Vector2(speed * (isFacingRight ? 1 : -1), rb.linearVelocity.y);
        animator.SetBool("isWalking", true);
    }
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    void ChasePlayer()
    {
        float chaseSpeed = speed * 1.5f;
        float direction = player.position.x > transform.position.x ? 1 : -1;
        if (direction < 0 && isFacingRight || direction > 0 && !isFacingRight)
        {
            Flip();
        }
        rb.linearVelocity = new Vector2(chaseSpeed * direction, rb.linearVelocity.y);
        animator.SetBool("isWalking", true);
    }
    void AttackPlayer()
    {
        if (isAttacking) return;
        isAttacking = true;
        animator.SetTrigger("Attack");
        rb.linearVelocity = Vector2.zero;
    }
    public void EndAttack()
    {
        isAttacking = false;
    }
    public void DealDamage()
    {
        Collider2D hit = Physics2D.OverlapCircle(wallCheck.position, checkRadius);
        if (hit != null && hit.CompareTag("Player"))
        {
            PlayerController pc = hit.GetComponent<PlayerController>();
            if (pc != null)
            {
                pc.TakeDamage(attackDamage);
            }
        }
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
        isAttacking = false;
        isHit = false;
        rb.linearVelocity = Vector2.zero;
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Hit");
        animator.SetTrigger("Die");
        gameManager.AddScore(1);
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = false; // Tắt collider khi chết
        }
        Destroy(gameObject, 2f);

    }
    public void EndHit()
    {
        isHit = false;
    }
    public void TakeDamage(int damage)
    {
        if (isDead) return;
        currentHealth -= damage;
        bloodBar.SetHealth(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        else
        {
            Hit();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            // Lấy điểm va chạm đầu tiên
            ContactPoint2D contact = collision.GetContact(0);
            // Nếu điểm va chạm nằm phía trên đầu Orc (so với tâm collider)
            if (contact.point.y > transform.position.y + 0.2f)
            {
                // Player bật lên
                Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (playerRb != null)
                {
                    playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, 10f); // 10f là lực bật lên, có thể chỉnh lại
                }
                TakeDamage(1);
            }
        }
    }
    
}
