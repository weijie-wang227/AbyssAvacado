using UnityEngine;

// Entities that can die should implement this interface
public interface IDeathHandler
{
    void HandleDeath();
}