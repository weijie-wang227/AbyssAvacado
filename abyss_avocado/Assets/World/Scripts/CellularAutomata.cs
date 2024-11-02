using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CellularAutomata
{
    private readonly int width;
    private readonly int height;
    private readonly int smoothSteps;
    private readonly int fillPercent;
    private readonly System.Random rng;

    public static readonly Vector2Int[] directions = { new(0, -1), new(0, 1), new(-1, 0), new(1, 0), new(-1, -1), new(-1, 1), new(1, -1), new Vector2Int(1, 1) };
    public static readonly Vector2Int[] orthogonalDirections = { new(0, -1), new(0, 1), new(-1, 0), new(1, 0) };

    public CellularAutomata(int width, int height, int smoothSteps, int fillPercent, System.Random rng)
    {
        this.width = width;
        this.height = height;
        this.smoothSteps = smoothSteps;
        this.fillPercent = fillPercent;
        this.rng = rng;
    }

    public bool[,] GenerateMap()
    {
        var map = new bool[height, width];
        
        RandomFill(map, rng, fillPercent);

        // Repeatedly smooth the map
        for (int i = 0; i < smoothSteps; i++)
        {
            map = SmoothMap(map);
        }

        var emptyRegions = GetRegions(map, false);

        // Remove regions smaller than 10 cells
        //PruneRegions(map, emptyRegions, 10);

        return map;
    }


    // Remove regions that are smaller than the threshold size
    // Regions are removed by inverting their bool value
    private void PruneRegions(bool[,] map, List<List<Vector2Int>> regions, int minRegionSize)
    {
        // Iterate over the list in reverse so that we can remove from it
        for (int i = regions.Count - 1; i >= 0; i--)
        {
            var region = regions[i];
            if (region.Count < minRegionSize)
            {
                foreach (var cell in region)
                {
                    map[cell.y, cell.x] = !map[cell.y, cell.x];
                }
                regions.Remove(region);
            }
        }
    }

    private void RandomFill(bool[,] map, System.Random rng, int fillPercent)
    {
        // Initialize map with random boolean values based on density percentage
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Map boundaries are always filled
                if (x == 0 || x == width - 1)
                {
                    map[y, x] = true;
                }
                else
                {
                    map[y, x] = rng.Next(0, 100) < fillPercent;
                }
            }
        }
    }
    private bool[,] SmoothMap(bool[,] map)
    {
        bool[,] mapCopy = new bool[height, width];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int filledNeighbors = CountFilledNeighbors(x, y, map);

                if (filledNeighbors > 4)
                {
                    mapCopy[y, x] = true;
                    // If the cell has less than "neighborThreshold" filled neighbors,
                    // then clear the cell
                }
                else if (filledNeighbors < 4)
                {
                    mapCopy[y, x] = false;
                }
                // Otherwise remain
                else
                {
                    mapCopy[y, x] = map[y, x];
                }
            }
        }

        return mapCopy;
    }

    // Get either the filled or empty regions on the map
    private List<List<Vector2Int>> GetRegions(bool[,] map, bool cellType)
    {
        var regions = new List<List<Vector2Int>>();
        bool[,] visited = new bool[height, width];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (map[y, x] != cellType || visited[y, x]) continue;

                var region = GetRegionCells(x, y, map);
                regions.Add(region);
                // Mark all cells in the region as visited
                foreach (var cell in region)
                {
                    visited[cell.y, cell.x] = true;
                }
            }
        }

        return regions;
    }

    // Use flood fill logic to get the coordinates of all cells connected to the given cell,
    // i.e. part of the same "region"
    private List<Vector2Int> GetRegionCells(int startX, int startY, bool[,] map)
    {
        // If the given cell is filled, find all connected filled cells
        // If the given cell is empty, find all connected empty cells
        bool cellType = map[startY, startX];

        var regionCells = new List<Vector2Int>(); // Maintain list of visited cells
        var queue = new Queue<Vector2Int>(); // Queue of cells to explore
        queue.Enqueue(new Vector2Int(startX, startY)); // Add start cell to queue

        bool[,] visited = new bool[height, width]; // Track which cells have already been visited   
        visited[startY, startX] = true;

        while (queue.Count != 0)
        {
            var cell = queue.Dequeue();
            regionCells.Add(cell); // Add current cell to region

            var neighbors = GetNeighbors(cell.x, cell.y, map, true);
            foreach (var neighbor in neighbors)
            {
                if (InBounds(neighbor, map) && !visited[neighbor.y, neighbor.x] && map[neighbor.y, neighbor.x] == cellType)
                {
                    queue.Enqueue(neighbor);
                    visited[neighbor.y, neighbor.x] = true;
                }
            }
        }

        return regionCells;
    }


    protected bool InBounds(Vector2Int cell, bool[,] map)
    {
        return InBounds(cell.y, cell.x, map);
    }
    protected bool InBounds(int x, int y, bool[,] map)
    {
        return x >= 0 && y >= 0 && x < width && y < height;
    }

    // Get neighboring cells in either 8-directions or only the 4 orthogonal directions
    // Does not check if neighbor is within map boundaries
    protected List<Vector2Int> GetNeighbors(int x, int y, bool[,] map, bool orthogonal = false)
    {
        List<Vector2Int> neighbors = new();

        Vector2Int[] dirs = orthogonal ? orthogonalDirections : directions;

        foreach (var dir in dirs)
        {
            Vector2Int neighbor = new(x + dir.x, y + dir.y);
            if (InBounds(neighbor, map))
            {
                neighbors.Add(neighbor);
            }
        }

        return neighbors;
    }

    // For the given cell position, count the number of neighboring cells that are filled
    protected int CountFilledNeighbors(int x, int y, bool[,] map)
    {
        int count = 0;

        for (int neighborX = x - 1; neighborX <= x + 1; neighborX++)
        {
            for (int neighborY = y - 1; neighborY <= y + 1; neighborY++)
            {
                if (x == neighborX && y == neighborY) continue;

                // Cells outside the x-bounds of the map are counted as filled
                if (neighborX <= 0 || neighborX >= width)
                {
                    count++;
                // Cells outside the y-bounds of the map are counted as unfilled
                } else if (neighborY <= 0 || neighborY >= height)
                {
                    continue;
                }
                else
                {
                    count += map[neighborY, neighborX] ? 1 : 0;
                }
            }
        }
        return count;
    }
}