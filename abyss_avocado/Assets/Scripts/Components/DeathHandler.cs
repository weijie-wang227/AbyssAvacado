using UnityEngine;

// Entities that can die should implement this interface
public abstract class DeathHandler : MonoBehaviour
{
    public abstract void HandleDeath();
}