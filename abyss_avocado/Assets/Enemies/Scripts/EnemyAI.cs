using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum State
    {
        Idle, Aggro
    }
    public State state = State.Idle;

    public void EnterAggro()
    {
        state = State.Aggro;
    }

    public void ExitAggro()
    {
        state = State.Idle;
    }
}