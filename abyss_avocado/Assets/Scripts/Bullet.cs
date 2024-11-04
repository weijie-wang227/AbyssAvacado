using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector] public float damage;
    [SerializeField] private float knockBack = 20f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<IDamageable>(out var target))
        {
            collision.rigidbody.AddForce(-collision.relativeVelocity.normalized * knockBack, ForceMode2D.Impulse);
            target.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
