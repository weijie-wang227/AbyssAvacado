using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

// Add this component to the player's InteractField, which contains a trigger collider
// that determines the radius in which the player can interact with interactables
public class InteractionManager : MonoBehaviour
{
    // Keep track of interactable objects within range
    private List<Interactable> trackedObjects = new();

    // The object that the interactor will interact with when the interact key is pressed
    public Interactable Target
    {
        // Target the object that is nearest to the interactor
        get
        {
            return trackedObjects.OrderBy(obj => CalculateDistance(obj)).FirstOrDefault();
        }
    }

    private void Update()
    {
        // When 'E' key is pressed, interact with the targeted interactable object, if any
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }

    }

    public void Interact()
    {
        if (Target != null)
        {
            print("Interacted with " + Target.name);
            Target.OnInteract();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent<Interactable>(out var obj))
        {
            AddObject(obj);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.TryGetComponent<Interactable>(out var obj))
        {
            RemoveObject(obj);
        }
    }

    // Add object to list of objects that are within range
    public void AddObject(Interactable obj)
    {
        if (!trackedObjects.Contains(obj))
        {
            trackedObjects.Add(obj);
        }
    }

    public void RemoveObject(Interactable obj)
    {
        if (trackedObjects.Contains(obj))
        {
            trackedObjects.Remove(obj);
        }
    }

    public float CalculateDistance(Interactable obj)
    {
        return Vector2.Distance(transform.position, obj.transform.position);
    }
}