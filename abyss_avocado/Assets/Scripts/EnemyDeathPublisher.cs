using System;
using UnityEngine;

// Singleton that broadcasts enemy deaths
public class EnemyDeathPublisher : MonoBehaviour
{
    public static EnemyDeathPublisher Instance { get; private set; }
    public event EventHandler EnemyDied;

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

    public void Publish()
    {
        EnemyDied?.Invoke(this, EventArgs.Empty);
    }
}