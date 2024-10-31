using UnityEngine;

public class Chunk
{
    public int index;
    public bool[,] grid;

    public Chunk(int index, bool[,] grid)
    {
        this.index = index;
        this.grid = grid;
    }
    
}