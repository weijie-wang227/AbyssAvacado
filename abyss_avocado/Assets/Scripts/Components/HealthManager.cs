using System;
using System.Collections;
using UnityEngine;

public class HealthManager : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth;
    private float health;

    [SerializeField] private float iframes = 1.0f;
    private bool invulnerable = false;
    public bool Invulnerable => invulnerable;

    float IDamageable.HitPoints => health;

    public event EventHandler HealthChanged;

    [SerializeField] private DeathHandler deathHandler;

    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (invulnerable) { return; }

        print($"{gameObject.name} received {damage} damage");

        if (damage < health)
        {
            health -= damage;
            StartCoroutine(InvulFrames()); // Entity gets invulnerability frames after taking damage
        }
        else
        {
            health = 0;
            deathHandler.HandleDeath();
        }
        HealthChanged?.Invoke(this, EventArgs.Empty);
    }

    private IEnumerator InvulFrames()
    {
        invulnerable = true;
        yield return new WaitForSeconds(iframes);
        invulnerable = false;
    }
    public void Heal(float amount)
    {
        health += Math.Min(amount, maxHealth - health);
        HealthChanged?.Invoke(this, EventArgs.Empty);
    }
}