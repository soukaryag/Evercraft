using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapRoomGenerator : AbstractDungeonGenerator
{
    public int mapSize;
    private int mapMiddle;
    public static int[,] mapLayout;

    public int roomSize;
    private int roomMiddle;
    private HashSet<Vector2Int> floorPositions;


    protected override void RunProceduralGeneration()
    {
        mapLayout = new int[mapSize, mapSize];
        floorPositions = new HashSet<Vector2Int>();
        mapMiddle = (int)(mapSize / 2) - 1;
        roomMiddle = (int)(roomSize / 2);

        CreateMapLayout();
        CreateRooms();
        CreateRoomTransitions();

        DrawTiles();
    }

    void CreateMapLayout()
    {
        CreateBasicShape();
        RemoveBlobsWithin();
        FillOutlineWithProbability();
        FillOutlineWithProbability();
        RemoveSurroundedRooms();
    }

    void CreateRooms()
    {
        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                if (mapLayout[i, j] == 1)
                {
                    RunAlgorithmicGeneration(new Vector2Int(i * roomSize, j * roomSize));
                }
            }
        }
    }

    void CreateRoomTransitions()
    {
        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                if (mapLayout[i, j] == 1)
                {
                    if (CoordsExist(i, j + 1) && mapLayout[i, j + 1] == 1)
                    {
                        CreateTransitionUp(i * roomSize, j * roomSize);
                    }

                    if (CoordsExist(i, j - 1) && mapLayout[i, j - 1] == 1)
                    {
                        CreateTransitionDown(i * roomSize, j * roomSize);
                    }

                    if (CoordsExist(i + 1, j) && mapLayout[i + 1, j] == 1)
                    {
                        CreateTransitionRight(i * roomSize, j * roomSize);
                    }

                    if (CoordsExist(i - 1, j) && mapLayout[i - 1, j] == 1)
                    {
                        CreateTransitionLeft(i * roomSize, j * roomSize);
                    }
                }
                    
            }
        }
    }

    void CreateTransitionUp(int x, int y)
    {
        for (int dy = 0; dy <= roomMiddle; dy++)
        {
            floorPositions.Add(new Vector2Int(x - 1, y + dy));
            floorPositions.Add(new Vector2Int(x, y + dy));
        }
    }

    void CreateTransitionDown(int x, int y)
    {
        for (int dy = 0; dy <= roomMiddle; dy++)
        {
            floorPositions.Add(new Vector2Int(x - 1, y - dy));
            floorPositions.Add(new Vector2Int(x, y - dy));
        }
    }

    void CreateTransitionRight(int x, int y)
    {
        for (int dx = 0; dx <= roomMiddle; dx++)
        {
            floorPositions.Add(new Vector2Int(x + dx, y - 1));
            floorPositions.Add(new Vector2Int(x + dx, y));
        }
    }

    void CreateTransitionLeft(int x, int y)
    {
        for (int dx = 0; dx <= roomMiddle; dx++)
        {
            floorPositions.Add(new Vector2Int(x - dx, y - 1));
            floorPositions.Add(new Vector2Int(x - dx, y));
        }
    }

    void CreateBasicShape()
    {
        for (int i = 0; i < 5; i++)
        {
            int x = mapMiddle;
            int y = mapMiddle;
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    FillMapPosition(x + dx, y + dy);
                }
            }


            while (true)
            {
                x = Random.Range(0, mapSize);
                y = Random.Range(0, mapSize);
                if (mapLayout[x, y] == 1)
                {
                    for (int dx = -1; dx <= 1; dx++)
                    {
                        for (int dy = -1; dy <= 1; dy++)
                        {
                            FillMapPosition(x + dx, y + dy);
                        }
                    }

                    break;
                }
            }

        }
    }

    void RemoveBlobsWithin()
    {
        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                if (Random.Range(0, 100) < 10)
                {
                    UnfillMapPosition(x, y);
                    UnfillMapPosition(x, y + 1);
                    UnfillMapPosition(x + 1, y);
                    UnfillMapPosition(x + 1, y + 1);
                }
            }
        }
    }

    void FillOutlineWithProbability()
    {
        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                if (mapLayout[i, j] == 1)
                {
                    FillMapPositionWithProbability(i + 1, j);
                    FillMapPositionWithProbability(i - 1, j);
                    FillMapPositionWithProbability(i, j + 1);
                    FillMapPositionWithProbability(i, j - 1);
                }
            }
        }
    }

    void RemoveSurroundedRooms()
    {
        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                if (
                    mapLayout[i, j] == 1 &&
                    IsFilledCell(i - 1, j - 1) &&
                    IsFilledCell(i - 1, j) &&
                    IsFilledCell(i - 1, j + 1) &&
                    IsFilledCell(i, j - 1) &&
                    IsFilledCell(i, j + 1) &&
                    IsFilledCell(i + 1, j - 1) &&
                    IsFilledCell(i + 1, j) &&
                    IsFilledCell(i + 1, j + 1)
                    )
                {
                    mapLayout[i, j] = 0;
                }
            }
        }
    }

    void FillMapPositionWithProbability(int x, int y)
    {
        if (!CoordsExist(x, y) || Random.Range(0, 100) < 70)
        {
            return;
        }

        mapLayout[x, y] = 1;
    }

    void FillMapPosition(int x, int y)
    {
        if (!CoordsExist(x, y))
        {
            return;
        }

        mapLayout[x, y] = 1;
    }

    void UnfillMapPosition(int x, int y)
    {
        if (!CoordsExist(x, y))
        {
            return;
        }

        mapLayout[x, y] = 0;
    }

    bool CoordsExist(int x, int y)
    {
        if (x < 0 || y < 0 || y >= mapSize || x >= mapSize)
        {
            return false;
        }

        return true;
    }

    bool IsFilledCell(int x, int y)
    {
        if (!CoordsExist(x, y)) return true;

        return mapLayout[x, y] == 1;
    }

    private void DrawTiles()
    {
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
    }

    private void RunProceduralGenerationFloor(Vector2Int startPosition)
    {
        RunAlgorithmicGeneration(startPosition);
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);

        // create teleporter
        // FloorTransitionGenerator floorTransitionGenerator = new FloorTransitionGenerator();
        // floorTransitionGenerator.CreateTeleporterOut(floorPositions.ElementAt(Random.Range(0, floorPositions.Count)), teleporterPrefab, teleporterInPrefab);

        // place torches
        //TorchPlacementGenerator torchPlacementGenerator = new TorchPlacementGenerator();
        // torchPlacementGenerator.Generate(floorPositions, tilemapVisualizer, flowMacro, 4);

        // generate loot chest
        // LootChestGenerator lootChestGenerator = new LootChestGenerator();
        // lootChestGenerator.Generate(floorPositions, chestPrefab);
    }

    protected void RunAlgorithmicGeneration(Vector2Int position)
    {
        var path = CellularAutomataDungeonGenerator.GenerateMap(position, roomSize - 1);
        floorPositions.UnionWith(path);
    }
}
