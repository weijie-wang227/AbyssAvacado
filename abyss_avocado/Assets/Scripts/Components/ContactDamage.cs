using Unity.VisualScripting;
using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    [HideInInspector] public bool isActive;
    [SerializeField] private float damage; // Damage dealt on contact
    [SerializeField] private LayerMask targetLayers;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isActive) return;
        var collider = collision.collider;
        
        if (InTargetLayers(collider.gameObject.layer) && collider.TryGetComponent<HealthManager>(out var obj))
        {
            obj.TakeDamage(damage);
        }
    }

    private bool InTargetLayers(int layer)
    {
        return (targetLayers & (1 << layer)) != 0;
    }
}