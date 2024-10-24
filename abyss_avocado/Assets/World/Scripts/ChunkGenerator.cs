using System.Collections.Generic;
using UnityEngine;

// Generates 1 chunk
public class ChunkGenerator : MonoBehaviour
{
    [SerializeField] protected int width; // Number of tile columns
    [SerializeField] protected int height; // Number of tile rows
    [SerializeField, Range(0, 100)] protected int fillPercent;
    [SerializeField] protected int smoothSteps;

    [SerializeField] protected string seed;
    [SerializeField] protected bool useRandomSeed;

    private CellularAutomata genAlgo;

    private int chunkLimit = 4;
    private Queue<Chunk> chunks = new();
    private Vector2 lastPosition; // Position of last chunk
    
    [SerializeField] private MapDisplay mapDisplay;
  
    void Start()
    {
        genAlgo = new CellularAutomata(width, height, smoothSteps, fillPercent);
        
        
    }

    private void Update()
    {
        // Keep track of player position
        // If player is halfway through the height of the current chunk, generate a new chunk

    }

    // Generate a chunk and display it on the map
    // Despawn old chunks if necessary
    public void LoadChunk() 
    {
        var chunk = GenerateChunk(lastPosition);
        chunks.Enqueue(chunk);
        mapDisplay.Create(chunk);

        // Despawn oldest chunk if necessary
        if (chunks.Count > chunkLimit)
        {
            var oldChunk = chunks.Dequeue();
            mapDisplay.Clear(oldChunk);
        }
    }

    public Chunk GenerateChunk(Vector2 position)
    {
        if (useRandomSeed)
        {
            seed = Time.time.ToString();
        }
        System.Random rng = new(seed.GetHashCode());

        var map = genAlgo.GenerateMap(width, height, smoothSteps, fillPercent, rng);

        var chunk = new Chunk(position, map);
        return chunk;
    }
}
