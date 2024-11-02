using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    [SerializeField] private List<WeightedElement<GameObject>> itemPrefabs;
    [SerializeField] private GameObject openedChestPrefab; // This will replace the chest after it has been opened i.e. interacted with

    public override void OnInteract(GameObject interactor)
    {
        var itemPickUp = RandomUtils.WeightedRandomSelect(itemPrefabs);
        Instantiate(itemPickUp, transform.position, transform.rotation);
        Destroy(gameObject);
        Instantiate(openedChestPrefab, transform.position, transform.rotation);
    }
}