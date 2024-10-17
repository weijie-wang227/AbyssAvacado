using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] protected int width; // Number of tile columns
    [SerializeField] protected int height; // Number of tile rows
    [SerializeField, Range(0, 100)] protected int fillPercent;
    [SerializeField] protected int smoothSteps;

    [SerializeField] protected string seed;
    [SerializeField] protected bool useRandomSeed;

    //private CellularAutomata cellularAutomataGenerator;
    private RandomWalker randomWalker;
    [SerializeField] private MapDisplay mapDisplay;
  
    void Start()
    {
        randomWalker = new RandomWalker(width, height, smoothSteps, fillPercent);
        Generate();        
    }

    public void Generate()
    {
        if (useRandomSeed)
        {
            seed = Time.time.ToString();
        }
        System.Random rng = new(seed.GetHashCode());

        var map = randomWalker.GenerateMap(width, height, smoothSteps, fillPercent, rng);

        mapDisplay.DisplayMap(map);
    }
}
