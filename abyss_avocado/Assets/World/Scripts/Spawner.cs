using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<WeightedElement<GameObject>> enemyPool;
    [SerializeField] private List<WeightedElement<GameObject>> chestPool;

    [SerializeField] private int baseEnemySpawnRate = 10; // Base number of enemies spawned per chunk
    [SerializeField] private int maxEnemySpawnRate = 20; // Max number of enemies spawned per chunk
    [SerializeField] private int chestSpawnRate = 2; // Chests spawned per chunk

    [SerializeField] private WorldGenerator worldGenerator;

    private int EnemySpawnRate
    {
        get
        {
            // Enemy spawn rate increases with number of chunks
            return Math.Min(baseEnemySpawnRate + worldGenerator.ChunkCount * 4, maxEnemySpawnRate);
        }
    }

    // Spawn enemies and items within the given chunk
    public void PopulateChunk(Chunk chunk)
    {
        var enemies = SpawnEntities(chunk, EnemySpawnRate, enemyPool);
        chunk.AddEntities(enemies);
        SpawnEntities(chunk, chestSpawnRate, chestPool);

    }

    // Spawn given number of entities from pool of entities at random positions within the chunk
    public List<GameObject> SpawnEntities(Chunk chunk, int count, List<WeightedElement<GameObject>> entityPool)
    {
        var emptyCells = chunk.GetEmptyCells();
        var entities = new List<GameObject>();
        
        for (int i  = 0; i < count; i++)
        {
            var spawnCell = RandomUtils.RandomSelect(emptyCells);
            var spawnPos = new Vector3(chunk.worldPos.x + spawnCell.x, chunk.worldPos.y - spawnCell.y, 0);
            var enemy = RandomUtils.WeightedRandomSelect(entityPool);
            entities.Add(SpawnEntity(enemy, spawnPos));
        }

        return entities;
    }

    private GameObject SpawnEntity(GameObject prefab, Vector3 position)
    {
        return Instantiate(prefab, position, Quaternion.identity);
    }
}