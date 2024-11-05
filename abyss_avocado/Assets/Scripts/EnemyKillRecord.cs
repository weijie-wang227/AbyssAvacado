using UnityEngine;

// Singleton that records enemy kills
public class EnemyKillRecord : MonoBehaviour
{
    public static EnemyKillRecord Instance { get; private set; }
    private int killCount = 0;
    public int KillCount => killCount;

    private void Awake()
    {
        // Singleton boilerplate
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

    }

    public void RecordKill()
    {
        killCount++;
    }
}