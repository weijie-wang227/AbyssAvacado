using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<WeightedElement<GameObject>> enemyPool;
    [SerializeField] private List<WeightedElement<GameObject>> chestPool;

    [SerializeField] private int baseEnemySpawnRate = 5; // Enemies spawned per chunk
    [SerializeField] private int chestSpawnRate = 2; // Chests spawned per chunk

    [SerializeField] private WorldGenerator worldGenerator;

    private int EnemySpawnRate
    {
        get
        {
            // Enemy spawn rate increases with number of chunks
            return baseEnemySpawnRate + worldGenerator.ChunkCount;
        }
    }

    // Spawn enemies and items within the given chunk
    public void PopulateChunk(Chunk chunk)
    {
        SpawnEntities(chunk, EnemySpawnRate, enemyPool);
        SpawnEntities(chunk, chestSpawnRate, chestPool);

    }

    // Spawn given number of entities from pool of entities at random positions within the chunk
    public void SpawnEntities(Chunk chunk, int count, List<WeightedElement<GameObject>> entityPool)
    {
        var emptyCells = chunk.GetEmptyCells();
        
        for (int i  = 0; i < count; i++)
        {
            var spawnCell = RandomUtils.RandomSelect(emptyCells);
            var spawnPos = new Vector3(chunk.worldPos.x + spawnCell.x, chunk.worldPos.y - spawnCell.y, 0);
            var enemy = RandomUtils.WeightedRandomSelect(entityPool);
            SpawnEntity(enemy, spawnPos);
        }
    }

    private void SpawnEntity(GameObject prefab, Vector3 position)
    {
        Instantiate(prefab, position, Quaternion.identity);
    }
}