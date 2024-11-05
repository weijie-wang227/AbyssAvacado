using System;
using System.Collections.Generic;
using UnityEngine;

// Generates 1 chunk
public class WorldGenerator : MonoBehaviour
{
    [SerializeField] private int chunkHeight; // Number of tile rows per chunk
    [SerializeField] private int chunkWidth; // Number of tile columns per chunk
    [SerializeField, Range(0, 100)] private int fillPercent;
    [SerializeField] private int smoothSteps;

    [SerializeField] private string seed;
    [SerializeField] private bool useRandomSeed;

    private CellularAutomata cellularAutomaton;

    private readonly int chunkLimit = 4;
    private readonly Queue<Chunk> chunks = new();
    private int currentIndex = 0; // Index of newest chunk
    public int ChunkCount => currentIndex; // Number of chunks generated so far
    private int NextLoadPosition => -chunkHeight * (currentIndex- 1) - chunkHeight / 2; // When player reaches this y position, load the next chunk

    private Player player; // Reference to player singleton

    [SerializeField] private MapDisplay mapDisplay; // Creates tilemap based on the grid

    [SerializeField] private Spawner spawner; // Manages spawning of enemies and items

    void Start()
    {
        player = Player.Instance;

        if (useRandomSeed)
        {
            seed = Guid.NewGuid().ToString();
        }

        System.Random rng = new(seed.GetHashCode());
        cellularAutomaton = new CellularAutomata(chunkWidth, chunkHeight, smoothSteps, fillPercent, rng);

        LoadChunk();
    }

    private void Update()
    {
        // Keep track of player position
        // If player is halfway through the height of the current chunk, generate a new chunk
        if (player.transform.position.y <= NextLoadPosition)
        {
            LoadChunk();
            Debug.Log("Chunk loaded");
        }
    }

    // Generate a chunk and display it on the map
    // Despawn old chunks if necessary
    public void LoadChunk() 
    {
        var chunk = GenerateChunk(currentIndex);
        chunks.Enqueue(chunk);
        mapDisplay.Create(chunk);
        spawner.PopulateChunk(chunk);

        // Despawn oldest chunk if necessary
        if (chunks.Count > chunkLimit)
        {
            var oldChunk = chunks.Dequeue();
            mapDisplay.Clear(oldChunk);
        }
        currentIndex += 1;
    }

    private Chunk GenerateChunk(int index)
    {
        var map = cellularAutomaton.GenerateMap();

        var chunk = new Chunk(index, new Vector2(0, -index * chunkHeight), map);
        return chunk;
    }
}
