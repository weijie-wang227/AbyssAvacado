using System;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    private void Start()
    {
        var playerHealth = Player.Instance.HealthManager;
        playerHealth.HealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(object sender, HealthEventArgs e)
    {
        var health = e.health;
        print("Health: " + health);
    }
}