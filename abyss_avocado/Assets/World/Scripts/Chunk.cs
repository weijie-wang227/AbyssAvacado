using UnityEngine;

public class Chunk
{
    private Vector2 position;
    private bool[,] grid;

    public Chunk(Vector2 position, bool[,] grid)
    {
        this.position = position;
        this.grid = grid;
    }
    
}