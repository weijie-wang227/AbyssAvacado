using System.Collections;
using UnityEngine;

// Attach this script to player gameobject
public class Player : MonoBehaviour, IDeathHandler
{
    public static Player Instance { get; private set; }
    public HealthManager HealthManager { get; private set; }

    private void Awake()
    {
        // Singleton boilerplate
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        HealthManager = GetComponent<HealthManager>();
    }

    public void HandleDeath()
    {
        print("Player died");
    }
}