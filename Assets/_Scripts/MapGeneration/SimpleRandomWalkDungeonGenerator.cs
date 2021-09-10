using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using Ludiq;
using Bolt;

public class SimpleRandomWalkDungeonGenerator : AbstractDungeonGenerator
{

    [SerializeField]
    protected SimpleRandomWalkSO randomWalkParameters;

    [SerializeField]
    protected GameObject chestPrefab;
    [SerializeField]
    protected GameObject teleporterPrefab;
    [SerializeField]
    protected GameObject teleporterInPrefab;
    [SerializeField]
    protected FlowMacro flowMacro;

    protected override void RunProceduralGeneration()
    {
        RunProceduralGenerationFloor(startPosition);
    }

    public void RunProceduralGenerationFloor(Vector2Int startPosition)
    {
        HashSet<Vector2Int> floorPositions = RunAlgorithmicGeneration(randomWalkParameters, startPosition);
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);

        // create teleporter
        FloorTransitionGenerator floorTransitionGenerator = new FloorTransitionGenerator();
        floorTransitionGenerator.CreateTeleporterOut(floorPositions.ElementAt(Random.Range(0, floorPositions.Count)), teleporterPrefab, teleporterInPrefab);

        // place torches
        TorchPlacementGenerator torchPlacementGenerator = new TorchPlacementGenerator();
        torchPlacementGenerator.Generate(floorPositions, tilemapVisualizer, flowMacro, 4);

        // generate loot chest
        // LootChestGenerator lootChestGenerator = new LootChestGenerator();
        // lootChestGenerator.Generate(floorPositions, chestPrefab);
    }

    protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkSO parameters, Vector2Int position)
    {
        var currentPosition = position;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>() { position };
        foreach (var direction in Direction2D.eightDirectionList)
        {
            floorPositions.Add(position + direction);
        }


        for (int i = 0; i < parameters.iterations; i++)
        {
            var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, parameters.walkLength);
            floorPositions.UnionWith(path);

            if (parameters.startRandomlyEachIteration)
            {
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            }
        }

        return floorPositions;
    }

    protected HashSet<Vector2Int> RunAlgorithmicGeneration(SimpleRandomWalkSO parameters, Vector2Int position)
    {
        var currentPosition = position;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>() { position };
        
        var path = CellularAutomataDungeonGenerator.GenerateMap(position, 64);
        floorPositions.UnionWith(path);

        return floorPositions;
    }
}
