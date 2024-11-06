using UnityEngine;

public class AggroManager : MonoBehaviour
{
    public enum State
    {
        Idle, Aggro
    }
    private State state = State.Idle;
    public State AggroState => state;

    [SerializeField] private LayerMask targetLayers;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (InTargetLayers(collider.gameObject))
        {
            state = State.Aggro;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (InTargetLayers(collider.gameObject))
        {
            state = State.Idle;
        }
    }

    private bool InTargetLayers(GameObject entity)
    {
        return (targetLayers & (1 << entity.layer)) != 0;
    }
}