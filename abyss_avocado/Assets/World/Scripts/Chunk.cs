using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Chunk
{
    public readonly int index;
    public readonly bool[,] grid; // Grid representing the empty/filled tiles of the chunk
    public readonly Vector2 worldPos; // World position of top-left corner of the chunk
    public readonly List<GameObject> entities = new();

    public Chunk(int index, Vector2 worldPos, bool[,] grid)
    {
        this.index = index;
        this.grid = grid;
        this.worldPos = worldPos;
    }
    
    public void AddEntities(List<GameObject> entities)
    {
        this.entities.AddRange(entities);
    }

    public void DespawnEntities()
    {
        foreach(var entity in entities)
        {
            Object.Destroy(entity);
        }
    }

    public List<Vector2Int> GetEmptyCells()
    {
        var emptyCells = new List<Vector2Int>();
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if (!grid[i, j])
                {
                    emptyCells.Add(new Vector2Int(j, i));
                }
            }
        }
        return emptyCells;
    }
}