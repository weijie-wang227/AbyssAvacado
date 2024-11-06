using UnityEngine;

public class EnemyDeathHandler : DeathHandler
{
    public override void HandleDeath()
    {
        Destroy(gameObject);
        // Death animation?
        if (RunStats.Instance != null )
        {
            RunStats.Instance.RecordKill();
        }
    }
}