using UnityEngine;

public class EnemyDeathHandler : MonoBehaviour, IDeathHandler
{
    public void HandleDeath()
    {
        Destroy(gameObject);
    }
}