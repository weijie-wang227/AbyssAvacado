using System;
using UnityEngine;

public class HealthManager : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth;
    private float health;

    float IDamageable.HitPoints => health;

    public event EventHandler HealthChanged;

    [SerializeField] private IDeathHandler deathHandler;

    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (damage < health)
        {
            health -= damage;
        }
        else
        {
            health = 0;
            deathHandler.HandleDeath();
        }
        HealthChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Heal(float amount)
    {
        health += Math.Min(amount, maxHealth - health);
        HealthChanged?.Invoke(this, EventArgs.Empty);
    }
}