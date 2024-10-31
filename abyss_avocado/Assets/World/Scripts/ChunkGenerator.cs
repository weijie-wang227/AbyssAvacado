using System.Collections.Generic;
using UnityEngine;

// Generates 1 chunk
public class ChunkGenerator : MonoBehaviour
{
    [SerializeField] private int rows; // Number of tile rows
    [SerializeField] private int cols; // Number of tile columns
    [SerializeField, Range(0, 100)] private int fillPercent;
    [SerializeField] private int smoothSteps;

    [SerializeField] private string seed;
    [SerializeField] private bool useRandomSeed;

    private CellularAutomata genAlgo;

    private int chunkLimit = 4;
    private Queue<Chunk> chunks = new();
    private int currentIndex = 0; // Index of newest chunk

    private Transform player; // Reference to player

    [SerializeField] private MapDisplay mapDisplay;
    // Get height of chunk in world units
    private int ChunkHeight => rows; // The height of 1 chunk in world units

    void Start()
    {
        genAlgo = new CellularAutomata(cols, rows, smoothSteps, fillPercent);

        LoadChunk();
    }

    private void Update()
    {
        // Keep track of player position
        // If player is halfway through the height of the current chunk, generate a new chunk
        //if (player.position.y <= lastPosition.y - ChunkHeight / 2)
        //{
        //    LoadChunk(lastPosition);
        //    lastPosition.y += ChunkHeight; // Update position for next chunk

        //}
    }

    // Generate a chunk and display it on the map
    // Despawn old chunks if necessary
    public void LoadChunk() 
    {
        var chunk = GenerateChunk(currentIndex);
        chunks.Enqueue(chunk);
        mapDisplay.Create(chunk);

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
        if (useRandomSeed)
        {
            seed = Time.time.ToString();
        }
        System.Random rng = new(seed.GetHashCode());

        var map = genAlgo.GenerateMap(cols, rows, smoothSteps, fillPercent, rng);

        var chunk = new Chunk(index, map);
        return chunk;
    }
}
