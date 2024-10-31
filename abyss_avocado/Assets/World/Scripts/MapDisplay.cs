using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapDisplay : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Vector2 position; // Bottom-left corner position

    [SerializeField] private TileBase fillTile;
    [SerializeField] private TileBase emptyTile;

    private void Awake()
    {
        tilemap.transform.position = position;
    }

    // Create tiles for new chunk
    public void Create(Chunk chunk)
    {
        var grid = chunk.grid;
        var yOffset = chunk.index * grid.GetLength(0);
        Debug.Log(yOffset);

        for (int y = 0; y < grid.GetLength(0); y++)
        {
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                var pos = new Vector3Int(x, y - yOffset);

                if (grid[y, x])
                {
                    tilemap.SetTile(pos, fillTile);
                }
                else
                {
                    tilemap.SetTile(pos, emptyTile);
                }

            }
        }
    }

    // Clear tiles to remove chunk
    public void Clear(Chunk chunk)
    {

    }

}