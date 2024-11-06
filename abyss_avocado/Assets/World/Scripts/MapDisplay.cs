using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapDisplay : MonoBehaviour
{
    [SerializeField] private Vector2 position; // top-left corner position

    [SerializeField] private Tilemap backgroundTilemap;
    [SerializeField] private Tilemap wallTilemap;

    [SerializeField] private TileBase wallTile;
    [SerializeField] private TileBase backgroundTile;

    private void Awake()
    {
        backgroundTilemap.transform.position = position;
    }

    // Check if the cell at the given world position is filled by a wall tile
    //public bool IsCellEmpty(Vector3 worldPos)
    //{
    //    wallTilemap.WorldToCell() {

    //    }
    //}

    // Create tiles for new chunk
    public void Create(Chunk chunk)
    {
        var grid = chunk.grid;
        var yOffset = chunk.index * grid.GetLength(0);

        for (int y = 0; y < grid.GetLength(0); y++)
        {
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                var pos = new Vector3Int(x, -y - 1 - yOffset);

                backgroundTilemap.SetTile(pos, backgroundTile);
                if (grid[y, x])
                {
                    wallTilemap.SetTile(pos, wallTile);
                }
            }
        }
    }

    // Clear tiles to remove chunk
    public void Clear(Chunk chunk)
    {

    }

}