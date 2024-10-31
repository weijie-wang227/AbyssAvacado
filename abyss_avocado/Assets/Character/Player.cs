using System.Collections;
using UnityEngine;

// Attach this script to player gameobject
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public int health = 10;
    public bool invulnerable = false;

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

    public void Damage(int damage)
    {

        health = health - damage;
        if (health < 0)
        {
            Death();
        }
        else
        {
            StartCoroutine(InvulFrames());
        }
    }

    private void Death()
    {

    }

    private IEnumerator InvulFrames()
    {
        invulnerable = true;
        yield return new WaitForSeconds(1.0f);
        invulnerable = false;
    }
}