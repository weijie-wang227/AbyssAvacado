using System;
using System.Collections;
using UnityEngine;

public class HealthManager : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth;
    private float health;
    private AudioSource audioSource;

    [SerializeField] private float iframes = 1.0f;
    private bool invulnerable = false;

    float IDamageable.HitPoints => health;

    public event EventHandler<HealthEventArgs> HealthChanged;

    [SerializeField] private DeathHandler deathHandler;

    void Start()
    {
        health = maxHealth;
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(float damage)
    {
        if (invulnerable) { return; }

        print($"{gameObject.name} received {damage} damage");
        
        if (audioSource != null)
        {
            audioSource.Play();
        }

        if (damage < health)
        {
            health -= damage;
            ActivateInvul();
        }
        else
        {
            health = 0;
            deathHandler.HandleDeath();
        }
        HealthChanged?.Invoke(this, new HealthEventArgs(health));
    }

    public void ActivateInvul()
    {
        if (invulnerable) return;
        StartCoroutine(InvulFrames()); 
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
        HealthChanged?.Invoke(this, new HealthEventArgs(health));
    }
}

public class HealthEventArgs
{
    public readonly float health;
    public HealthEventArgs(float health)
    {
        this.health = health;
    }
}