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

    private MapGenerator mapGenerator;

    private readonly int chunkLimit = 2;
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
        mapGenerator = new MapGenerator(chunkWidth, chunkHeight, smoothSteps, fillPercent, rng);

        LoadChunk();
    }

    private void Update()
    {
        if (player ==  null) { return; }
        // Keep track of player position
        // If player is halfway through the height of the current chunk, generate a new chunk
        if (player.transform.position.y <= NextLoadPosition)
        {
            LoadChunk();
        }
    }

    // Generate a chunk and display it on the map
    // Despawn old chunks if necessary
    public void LoadChunk() 
    {
        var map = mapGenerator.GenerateMap();   
        var chunk = new Chunk(currentIndex, new Vector2(0, -currentIndex * chunkHeight), map);
        chunks.Enqueue(chunk);
        mapDisplay.Create(chunk);
        spawner.PopulateChunk(chunk);

        if (chunks.Count > chunkLimit)
        {
            var oldChunk = chunks.Dequeue();
            oldChunk.DespawnEntities();
        }
        currentIndex += 1;

        print($"Chunk loaded. Chunks: {ChunkCount}");
    }
}
