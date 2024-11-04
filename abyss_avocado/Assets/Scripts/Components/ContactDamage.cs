using Unity.VisualScripting;
using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    [SerializeField] private float contactDamage; // Damage dealt on contact
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<IDamageable>(out var obj))
        {
            obj.TakeDamage(contactDamage);
        }
    }
}