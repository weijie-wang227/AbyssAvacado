using UnityEngine;

// Singleton that records enemy kills and chunk depth
public class RunStats : MonoBehaviour
{
    public static RunStats Instance { get; private set; }
    private int killCount = 0;
    public int KillCount => killCount;

    private int depth = 0;
    public int Depth => depth;

    private void Awake()
    {
        // Singleton boilerplate
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this); 
    }

    public void RecordDepth()
    {
        depth = -(int)Player.Instance.transform.position.y;
    }

    public void RecordKill()
    {
        killCount++;
    }

    public void Reset()
    {
        killCount = 0;
        depth = 0;
    }
}