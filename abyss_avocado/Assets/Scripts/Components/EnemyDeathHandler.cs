using UnityEngine;

public class EnemyDeathHandler : DeathHandler
{
    void Start()
    {
    }
    public override void HandleDeath()
    {
        Destroy(gameObject);
    }
}