using UnityEngine;

public class Decay : MonoBehaviour
{
    [SerializeField] private int rate = 1; // How fast to descend 
    [SerializeField] private LayerMask targetLayers; 

    private void Update()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - rate * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var obj = collision.collider.gameObject;
        if (InTargetLayers(obj.layer) && obj.TryGetComponent<HealthManager>(out var entity))
        {
            entity.TakeDamage(999);
        }
    }

    private bool InTargetLayers(int layer)
    {
        return (targetLayers & (1 << layer)) != 0;
    }
}