// using UnityEngine;
// using System.Collections;

// public class MovingPlatform : MonoBehaviour
// {
//     [SerializeField] private Transform pointA;
//     [SerializeField] private Transform pointB;
//     [SerializeField] private float speed = 2f;
//     private Vector3 target;
//     private Transform player;
//     void Start()
//     {
//         target = pointA.position;
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         // Move the platform towards the target position
//         transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

//         // Check if the platform has reached the target position
//         if (Vector3.Distance(transform.position, target) < 0.1f)
//         {
//             if (target == pointA.position)
//             {
//                 target = pointB.position; // Switch to point B
//             }
//             else
//             {
//                 target = pointA.position; // Switch to point A
//             }
//         }
//     }
//     private void OnCollisionEnter2D(Collision2D collision)
//     {
//         if (collision.gameObject.CompareTag("Player"))
//         {
//             collision.transform.SetParent(transform);
//         }
//     }

//     private void OnCollisionExit2D(Collision2D collision)
//     {
//         if (collision.gameObject.CompareTag("Player"))
//         {
//             StartCoroutine(UnparentAfterDelay(collision.transform));
//         }
//     }
//     private IEnumerator UnparentAfterDelay(Transform player)
//     {
//         yield return new WaitForSeconds(0.1f);
//         player.SetParent(null); 
//     }
// }
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed = 2f;
    private Vector3 target;
    private Vector3 lastPosition;

    void Start()
    {
        target = pointA.position;
        lastPosition = transform.position;
    }

    void Update()
    {
        // Move the platform towards the target position
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Check if the platform has reached the target position
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            if (target == pointA.position)
                target = pointB.position;
            else
                target = pointA.position;
        }
    }

    void FixedUpdate()
    {
        lastPosition = transform.position;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                // Tính vận tốc của platform
                Vector3 platformVelocity = (transform.position - lastPosition) / Time.fixedDeltaTime;
                // Cộng vận tốc platform vào player (chỉ trục X)
                playerRb.linearVelocity += new Vector2(platformVelocity.x, 0);
            }
        }
    }
}