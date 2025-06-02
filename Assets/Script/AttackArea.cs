using UnityEngine;

public class AttackArea : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<OrcAI>().TakeDamage(1);
        }
    }
}
