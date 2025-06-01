using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyPatrol : MonoBehaviour
{
    public float moveSpeed = 2f; // tốc độ chạy
    public Transform groundCheck; // điểm kiểm tra mép đất
    public Transform wallCheck; // điểm kiểm tra tường
    public LayerMask groundLayer; // layer của mặt đất

    private Rigidbody2D rb;
    private bool movingRight = true;

    public float groundCheckDistance = 0.2f; // khoảng cách kiểm tra mặt đất
    public float wallCheckDistance = 0.2f; // khoảng cách kiểm tra tường

    private GameManager gameManager;
    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Patrol();
    }

    void Patrol()
    {
        // Di chuyển theo hướng hiện tại
        float direction = movingRight ? 1f : -1f;
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);

        // Kiểm tra nếu phía trước không còn mặt đất → quay đầu
        RaycastHit2D groundInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
        if (!groundInfo.collider)
        {
            Flip();
        }

        // Kiểm tra nếu phía trước có tường → quay đầu
        RaycastHit2D wallInfo = Physics2D.Raycast(wallCheck.position, Vector2.right * (movingRight ? 1 : -1), wallCheckDistance, groundLayer);
        if (wallInfo.collider)
        {
            Flip();
        }
    }

    void Flip()
    {
        movingRight = !movingRight;

        // Quay mặt sprite
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
    public void TakeDamage(int damage)
    {
    // Có thể thêm hiệu ứng trúng đòn ở đây
    Destroy(gameObject);
    }

    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (gameManager != null && gameManager.IsGameOver()) return;
    //     if(collision.gameObject.CompareTag("Player"))
    //     {
    //         Vector2 contactPoint = collision.GetContact(0).point; // Lấy điểm va chạm
    //         Vector2 colliderCenter = GetComponent<Collider2D>().bounds.center; // Lấy tâm của enemy collider
    //         Vector2 colliderSize = GetComponent<Collider2D>().bounds.size; // Lấy kích thước của enemy collider
    //         Vector2 colliderOffset = GetComponent<Collider2D>().offset; // Lấy offset của enemy collider

    //         print("contactPoint: " + contactPoint.y + " " + contactPoint.x);
    //         print("enemyCenter: " + colliderCenter.y + " " + colliderCenter.x);
    //         print("enemySize: " + colliderSize.y + " " + colliderSize.x);

    //         bool yCheck = contactPoint.y > colliderCenter.y;
    //         bool xCheck = contactPoint.x > colliderCenter.x - colliderSize.x / 2 && contactPoint.x < colliderCenter.x + colliderSize.x / 2;

    //         // if (contactPoint.y > colliderCenter.y && contactPoint.x > colliderCenter.x - colliderSize.x / 2 && contactPoint.x < colliderCenter.x + colliderSize.x / 2)
    //         // {
    //         //     // Nếu va chạm từ trên xuống
    //         //     gameManager.AddScore(1); // Gọi hàm AddScore trong GameManager
    //         //     Destroy(gameObject); // Hủy enemy
    //         // }
    //         if (yCheck && xCheck)
    //         {
    //             // Nếu va chạm từ trên xuống
    //             gameManager.AddScore(1); // Gọi hàm AddScore trong GameManager
    //             Destroy(gameObject); // Hủy enemy
    //         }
    //         else
    //         {
    //             print(yCheck + " " + xCheck);
    //             gameManager.GameOver(); // Gọi hàm GameOver trong GameManager
    //         }
    //     }
    // }

    void OnCollisionEnter2D(Collision2D collision)
    {
    if (gameManager != null && gameManager.IsGameOver()) return;

    if (collision.gameObject.CompareTag("Player"))
    {
        gameManager.GameOver(); // Gọi hàm GameOver trong GameManager
    }
    }
}
