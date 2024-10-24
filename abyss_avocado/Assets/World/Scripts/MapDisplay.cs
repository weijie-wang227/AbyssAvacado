using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapDisplay : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Vector2 position; // Bottom-left corner position

    [SerializeField] private TileBase floorTile;
    [SerializeField] private TileBase wallTile;

    private void Awake()
    {
        tilemap.transform.position = position;
    }

    // Create tiles for new chunk
    public void Create(Chunk chunk)
    {

    }

    // Clear tiles to remove chunk
    public void Clear(Chunk chunk)
    {

    }

    public void Display(TileType[,] map)
    {
        // Clear any existing tiles before redrawing
        tilemap.ClearAllTiles();
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                var pos = new Vector3Int(x, y);

                switch (map[x, y])
                {
                    case TileType.FLOOR:
                        tilemap.SetTile(pos, floorTile);
                        break;
                    case TileType.WALL:
                        tilemap.SetTile(pos, wallTile);
                        break;
                    case TileType.EMPTY:
                        break;
                }

            }
        }
    }

    public void Display(bool[,] map)
    {
        // Clear any existing tiles before redrawing
        tilemap.ClearAllTiles();
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                var pos = new Vector3Int(x, y);

                if (map[x, y])
                {
                    tilemap.SetTile(pos, wallTile);
                }
                else
                {
                    tilemap.SetTile(pos, floorTile);
                }

            }
        }
    }
}