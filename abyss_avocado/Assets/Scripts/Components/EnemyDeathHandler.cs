using UnityEngine;

public class EnemyDeathHandler : DeathHandler
{
    public override void HandleDeath()
    {
        Destroy(gameObject);
        // Death animation?
        EnemyDeathPublisher.Instance.Publish();
    }
}