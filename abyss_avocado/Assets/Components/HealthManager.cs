using System;
using UnityEngine;

public class HealthManager : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth;
    private float health;

    float IDamageable.HitPoints => health;

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
            Die();
        }
    }

    public void Heal(float _health)
    {
        health += Math.Min(_health, maxHealth - health); // prevent overhealing
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " died");
    }

    void IDamageable.OnDestroyed() => Die();
}