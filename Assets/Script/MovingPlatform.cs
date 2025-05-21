using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
   [SerializeField] private Transform pointA; 
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed = 2f;
    private Vector3 target;
    void Start()
    {
        target = pointA.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the platform towards the target position
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Check if the platform has reached the target position
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            // Switch the target between point A and point B
            target = (target == pointA.position) ? pointB.position : pointA.position;
        }
    }
}
