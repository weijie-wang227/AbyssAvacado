using UnityEngine;

public class HealthPack : Interactable
{
    [SerializeField] private float healAmount = 5f;

    public override void OnInteract()
    {
        Player.Instance.HealthManager.Heal(healAmount);
        Destroy(gameObject);
    }
}