using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using Ludiq;
using Bolt;

public class CorridorFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{

    [SerializeField]
    private int corridorLength = 14, corridorCount = 5;
    [SerializeField]
    [Range(0.1f,1)]
    private float roomPercent = 0.8f;

    protected override void RunProceduralGeneration() {
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration() {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

        CreateCorridors(floorPositions, potentialRoomPositions);

        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);
        CreateRoomsAtDeadEnd(deadEnds, roomPositions);

        floorPositions.UnionWith(roomPositions);

        foreach (var direction in Direction2D.eightDirectionList) {
            floorPositions.Add(new Vector2Int(0, 0) + direction);
        }

        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
        
        // create teleporter
        FloorTransitionGenerator floorTransitionGenerator = new FloorTransitionGenerator(); 
        floorTransitionGenerator.CreateTeleporterOut(floorPositions.ElementAt(Random.Range(0, floorPositions.Count)), teleporterPrefab, teleporterInPrefab);

        // place torches
        TorchPlacementGenerator torchPlacementGenerator = new TorchPlacementGenerator(); 
        torchPlacementGenerator.Generate(floorPositions, tilemapVisualizer, flowMacro, 8);

        // generate loot chest
        LootChestGenerator lootChestGenerator = new LootChestGenerator();
        lootChestGenerator.Generate(floorPositions, chestPrefab);
    }

    private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors) {
        foreach ( var position in deadEnds ) {
            if (roomFloors.Contains(position) == false) {
                var room = RunRandomWalk(randomWalkParameters, position);
                roomFloors.UnionWith(room);
            }
        }
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions) {
        List<Vector2Int> deadEnds = new List<Vector2Int>();

        foreach (var position in floorPositions) {
            int neighborsCount = 0;
            foreach (var direction in Direction2D.cardinalDirectionList) {
                if (floorPositions.Contains(position + direction)) {
                    neighborsCount++;
                }
            }

            if (neighborsCount == 1) {
                deadEnds.Add(position);
            }
        }

        return deadEnds;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions) {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);

        List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

        int treasureRoomIdx = Random.Range(1, roomsToCreate.Count);
        Vector2Int treasureRoomCenter = roomsToCreate.ElementAt(treasureRoomIdx);

        int i = 0;
        foreach (var roomPosition in roomsToCreate) {
            if (i == treasureRoomIdx) continue;

            var roomFloor = RunRandomWalk(randomWalkParameters, roomPosition);
            roomPositions.UnionWith(roomFloor);

            i += 1;
        }

        HashSet<Vector2Int> trasureRoomFloorPositions = CreateTreasureRoom(treasureRoomCenter);
        roomPositions.UnionWith(trasureRoomFloorPositions);

        return roomPositions;
    }

    private HashSet<Vector2Int> CreateTreasureRoom(Vector2Int treasureRoomCenter) {
        HashSet<Vector2Int> trasureRoomFloorPositions = new HashSet<Vector2Int>();
        for (int j = -3; j <= 3; j++) {
            for (int k = -3; k <= 3; k++) {
                trasureRoomFloorPositions.Add(treasureRoomCenter + new Vector2Int(j, k));
            }
        }

        return trasureRoomFloorPositions;
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions) {
        var currentPosition = startPosition;
        potentialRoomPositions.Add(currentPosition);


        for (int i = 0; i < corridorCount; i++) {
            var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, corridorLength);
            currentPosition = corridor[corridor.Count - 1];
            potentialRoomPositions.Add(currentPosition);
            floorPositions.UnionWith(corridor);
        }
    }
}
