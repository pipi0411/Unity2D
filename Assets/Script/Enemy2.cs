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
}
