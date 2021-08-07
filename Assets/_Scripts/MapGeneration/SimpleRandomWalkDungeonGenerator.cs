using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRandomWalkDungeonGenerator : AbstractDungeonGenerator
{

    [SerializeField]
    protected SimpleRandomWalkSO randomWalkParameters;

    [SerializeField]
    protected Sprite coinSprite;
    [SerializeField]
    protected RuntimeAnimatorController coinAnimation;

    protected override void RunProceduralGeneration()
    {
        RunProceduralGenerationFloor(new Vector2Int(0, 0));
        RunProceduralGenerationFloor(new Vector2Int(50, 0));
        RunProceduralGenerationFloor(new Vector2Int(100, 0));
    }

    protected void RunProceduralGenerationFloor(Vector2Int startPosition) {
        HashSet<Vector2Int> floorPositions = RunRandomWalk(randomWalkParameters, startPosition);
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
        FloorTransitionGenerator.CreateLadderDown(floorPositions.ElementAt(Random.Range(0, floorPositions.Count)), tilemapVisualizer);

        // place torches
        TorchPlacementGenerator torchPlacementGenerator = new TorchPlacementGenerator(); 
        torchPlacementGenerator.Generate(floorPositions, tilemapVisualizer);

        // generate coin loot
        CoinSpawnGenerator coinSpawnGenerator = new CoinSpawnGenerator();
        coinSpawnGenerator.Generate(floorPositions, coinSprite, coinAnimation);
    }

    protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkSO parameters, Vector2Int position)
    {
        var currentPosition = position;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
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

}
