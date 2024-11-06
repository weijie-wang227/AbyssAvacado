using UnityEngine;

public class AggroZone : MonoBehaviour
{
    [SerializeField] private EnemyAI enemyAI;
    [SerializeField] private LayerMask targetLayers;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (InTargetLayers(collider.gameObject))
        {
            enemyAI.EnterAggro();
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (InTargetLayers(collider.gameObject))
        {
            enemyAI.ExitAggro();
        }
    }

    private bool InTargetLayers(GameObject entity)
    {
        return (targetLayers & (1 << entity.layer)) != 0;
    }
}