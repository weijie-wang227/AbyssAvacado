using UnityEngine;

// Attach this script to player gameobject
public class Player : MonoBehaviour 
{
    public static Player Instance { get; private set; }

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
}