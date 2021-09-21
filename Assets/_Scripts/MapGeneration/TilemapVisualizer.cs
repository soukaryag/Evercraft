using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private TileBase[] floorTiles;
    [SerializeField]
    private TileBase[] floorOutskirtsTiles;    
    [SerializeField]
    private TileBase[] floorDecor;
    [SerializeField]
    private TileBase[] floorDecorShadows;
    [SerializeField]
    private Tilemap floorTilemap, floorDecorTilemap, floorDecorShadowTilemap, wallTilemap, torchTilemap;
    [SerializeField]
    private TileBase wallTop, wallSideRight, wallSideLeft, wallBottom, wallFull, 
        wallInnerCornerDownLeft, wallInnerCornerDownRight,
        wallDiagonalCornerDownLeft, wallDiagonalCornerDownRight,
        wallDiagonalCornerUpRight, wallDiagonalCornerUpLeft,
        wallTopLeftRight,
        torchLeftTile, torchTopTile, torchRightTile;

    public Tilemap getTorchTilemap() {
        return torchTilemap;
    }

    public void ShiftFloorTilemapDown() {
        floorTilemap.transform.position -= new Vector3(0, 0.5f, 0); 
    }

    public void PaintFloorOutskirtsTiles(int[,] mapLayout, int mapSize, int roomSize) {

        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                if (mapLayout[i, j] == 1)
                {
                    int x = i * 2 * roomSize;
                    int y = j * 2 * roomSize;
                    for (int dx = -1*roomSize; dx < roomSize; dx++) {
                        for (int dy = -1*roomSize; dy < roomSize; dy++) {
                            int idx = Random.Range(0, floorOutskirtsTiles.Length);
                            PaintSingleTile(floorTilemap, floorOutskirtsTiles[idx], new Vector2Int(x + dx, y + dy));
                        }
                    }
                }
            }
        }
    }

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositionsEnumerable) {

        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        floorPositions.UnionWith(floorPositionsEnumerable);

        foreach (var position in floorPositions) {
            int idx = Random.Range(0, floorTiles.Length);
            PaintSingleTile(floorTilemap, floorTiles[idx], position);
        }
    }

    public void PaintFloorDecor(IEnumerable<Vector2Int> floorPositionsEnumerable)
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        floorPositions.UnionWith(floorPositionsEnumerable);

        foreach (var position in floorPositions)
        {
            if (Random.Range(0, 100) < 5)
            {
                if (!floorPositions.Contains(new Vector2Int(position.x, position.y + 1))) continue;
                if (!floorPositions.Contains(new Vector2Int(position.x + 1, position.y))) continue;

                int idx = Random.Range(3, floorDecor.Length);
                PaintSingleTile(floorDecorTilemap, floorDecor[idx], position);

                if (idx < floorDecorShadows.Length)
                {
                    PaintSingleTileFloat(
                        floorDecorShadowTilemap, 
                        floorDecorShadows[idx], 
                        new Vector3(position.x, position.y - 0.001f, 0)
                        );
                }
            }
        }
    }

    public void PaintOuterDecor(IEnumerable<Vector2Int> floorPositionsEnumerable)
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        floorPositions.UnionWith(floorPositionsEnumerable);

        foreach (var position in floorPositions)
        {
            if (Random.Range(0, 100) < 5)
            {
                var newLoc = new Vector2Int(position.x + 5, position.y);
                PaintOuterDecorHelper(floorPositions, newLoc);

                newLoc = new Vector2Int(position.x - 5, position.y);
                PaintOuterDecorHelper(floorPositions, newLoc);
                
            }
        }
    }

    public void PaintOuterDecorHelper(HashSet<Vector2Int> floorPositions, Vector2Int newLoc) {
        if (!floorPositions.Contains(newLoc)) {
            foreach ( var direction in Direction2D.cardinalDirectionList ) {
                if (floorPositions.Contains(newLoc + direction)) return;
            }

            int idx = Random.Range(0, 3);
            PaintSingleTile(floorDecorTilemap, floorDecor[idx], newLoc);

            if (idx < floorDecorShadows.Length)
            {
                PaintSingleTile(
                    floorDecorShadowTilemap,
                    floorDecorShadows[idx],
                    newLoc
                    );
            }
        }
    }

    public void PaintTorchLeft(IEnumerable<Vector2Int> positions) {
        PaintTiles(positions, torchTilemap, torchLeftTile);
    }

    public void PaintTorchRight(IEnumerable<Vector2Int> positions) {
        PaintTiles(positions, torchTilemap, torchRightTile);
    }

    public void PaintTorchTop(IEnumerable<Vector2Int> positions) {
        PaintTiles(positions, torchTilemap, torchTopTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile) {
        foreach (var position in positions) {
            PaintSingleTile(tilemap, tile, position);
        }
    }

    internal void PaintSingleBasicWall(Vector2Int position, string binaryType) {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;
        if (WallTypeHelper.wallTop.Contains(typeAsInt)) {
            tile = wallTop;
            if (WallTypeHelper.wallTopException.Contains(typeAsInt)) {
                PaintSingleTile(wallTilemap, wallTopLeftRight, new Vector2Int(position.x, position.y + 1));
            }
        } else if (WallTypeHelper.wallSideRight.Contains(typeAsInt)) {
            tile = wallSideRight;
        } else if (WallTypeHelper.wallSideLeft.Contains(typeAsInt)) {
            tile = wallSideLeft;
        } else if (WallTypeHelper.wallBottm.Contains(typeAsInt)) {
            tile = wallBottom;
        } else if (WallTypeHelper.wallFull.Contains(typeAsInt)) {
            tile = wallFull;
        }

        if (tile != null)
            PaintSingleTile(wallTilemap, tile, position);
    }

    internal void PaintSingleCornerWall(Vector2Int position, string binaryType) {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;
        if (WallTypeHelper.wallInnerCornerDownLeft.Contains(typeAsInt)) {
            tile = wallInnerCornerDownLeft;
        } else if (WallTypeHelper.wallTopLeftRight.Contains(typeAsInt)) {
            tile = wallTopLeftRight;
        } else if (WallTypeHelper.wallInnerCornerDownRight.Contains(typeAsInt)) {
            tile = wallInnerCornerDownRight;
        } else if (WallTypeHelper.wallDiagonalCornerDownLeft.Contains(typeAsInt)) {
            tile = wallDiagonalCornerDownLeft;
        } else if (WallTypeHelper.wallDiagonalCornerDownRight.Contains(typeAsInt)) {
            tile = wallDiagonalCornerDownRight;
        } else if (WallTypeHelper.wallDiagonalCornerUpRight.Contains(typeAsInt)) {
            tile = wallDiagonalCornerUpRight;
        } else if (WallTypeHelper.wallDiagonalCornerUpLeft.Contains(typeAsInt)) {
            tile = wallDiagonalCornerUpLeft;
        } else if (WallTypeHelper.wallFullEightDirections.Contains(typeAsInt)) {
            tile = wallFull;
        } else if (WallTypeHelper.wallBottmEightDirections.Contains(typeAsInt)) {
            tile = wallBottom;
        }

        if (tile != null)
            PaintSingleTile(wallTilemap, tile, position);
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position) {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }

    private void PaintSingleTileFloat(Tilemap tilemap, TileBase tile, Vector3 position)
    {
        var tilePosition = tilemap.WorldToCell(position);
        tilemap.SetTile(tilePosition, tile);
    }

    public void Clear() {
        floorTilemap.ClearAllTiles();
        floorDecorTilemap.ClearAllTiles();
        floorDecorShadowTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
        torchTilemap.ClearAllTiles();

        int childs = torchTilemap.transform.childCount;
        for (int i = childs - 1; i >= 0; i--) {
            GameObject.DestroyImmediate( torchTilemap.transform.GetChild( i ).gameObject );
        }

        string[] parents = new string[] {"ChestParent", "LootDropParent", "RoomExitParent"};

        foreach (string parent in parents) {
            GameObject PARENT_OBJECT = GameObject.Find(parent);
            int child = PARENT_OBJECT.transform.childCount;
            for (int i = child - 1; i >= 0; i--) {
                GameObject.DestroyImmediate( PARENT_OBJECT.transform.GetChild( i ).gameObject );
            }
        }
        
    }
}
