using System;
using System.Collections;
using UnityEngine;

// Attach this script to player gameobject
public class Player : DeathHandler
{
    public static Player Instance { get; private set; }
    [field: SerializeField] public HealthManager HealthManager { get; private set; }
    public event EventHandler PlayerDied;

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

    public override void HandleDeath()
    {
        print("Player died");
        PlayerDied?.Invoke(this, EventArgs.Empty);
    }
}