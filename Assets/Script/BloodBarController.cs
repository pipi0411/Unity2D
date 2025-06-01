using UnityEngine;

public class BloodBarController : MonoBehaviour
{
    [SerializeField] private Sprite[] bloodBarSprites;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void SetHealth(float currentHealth, float maxHealth)
    {
        float percent = currentHealth / maxHealth;
        int index = Mathf.Clamp(Mathf.FloorToInt(percent * (bloodBarSprites.Length - 1)), 0, bloodBarSprites.Length - 1);
        spriteRenderer.sprite = bloodBarSprites[index];
    }
}
