using UnityEngine;

 
public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 2f; 
    [SerializeField] private float distance = 5f;
    private Vector3 startPos;
    private bool movingRight = true; // Flag to check the direction of movement
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float leftBound = startPos.x - distance; // Calculate the left boundary
        float rightBound = startPos.x + distance; // Calculate the right boundary
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime; // Move right
            if (transform.position.x >= rightBound) // Check if the enemy has reached the right boundary
            {
                movingRight = false; // Change direction to left
                Flip(); // Flip the enemy sprite
            }
        }
        else
        {
            transform.position -= Vector3.right * speed * Time.deltaTime; // Move left
            if (transform.position.x <= leftBound) // Check if the enemy has reached the left boundary
            {
                movingRight = true; // Change direction to right
                Flip(); // Flip the enemy sprite
            }
        }
    }
    void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1; // Flip the sprite horizontally
        transform.localScale = localScale;
    }
}
