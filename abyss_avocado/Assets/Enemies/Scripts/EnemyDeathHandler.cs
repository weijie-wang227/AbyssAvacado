using UnityEngine;

public class EnemyDeathHandler : DeathHandler
{
    [SerializeField] private GameObject deathEffect;
    public override void HandleDeath()
    {
        Destroy(gameObject);
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        if (RunStats.Instance != null )
        {
            RunStats.Instance.RecordKill();
        }
    }
}