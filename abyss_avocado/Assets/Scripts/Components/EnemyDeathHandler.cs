using UnityEngine;

public class EnemyDeathHandler : DeathHandler
{
    public override void HandleDeath()
    {
        Destroy(gameObject);
        // Death animation?
        RunStats.Instance.RecordKill();
    }
}