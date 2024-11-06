using Unity.VisualScripting;
using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    [SerializeField] public bool isActive;
    [SerializeField] private float damage; // Damage dealt on contact

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isActive) return;
        var collider = collision.collider;
        if (collider.CompareTag("Player") && collider.TryGetComponent<IDamageable>(out var obj))
        {
            obj.TakeDamage(damage);
        }
    }
}