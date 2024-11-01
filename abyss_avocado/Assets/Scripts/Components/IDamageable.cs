public interface IDamageable
{
    float HitPoints { get; }
    void TakeDamage(float damage);
}