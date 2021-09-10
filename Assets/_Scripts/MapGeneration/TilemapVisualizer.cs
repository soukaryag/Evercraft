using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap, wallTilemap, torchTilemap;
    [SerializeField]
    private TileBase floorTile0, floorTile1_1, floorTile1_2, floorTile1_3, floorTile2_1, floorTile2_2, 
        floorTile3, floorTile4, floorTile5, 
        wallTop, wallSideRight, wallSideLeft, wallBottom, wallFull, 
        wallInnerCornerDownLeft, wallInnerCornerDownRight,
        wallDiagonalCornerDownLeft, wallDiagonalCornerDownRight,
        wallDiagonalCornerUpRight, wallDiagonalCornerUpLeft,
        ladderUpTile, ladderDownTile, torchLeftTile, torchTopTile, torchRightTile;

    public Tilemap getTorchTilemap() {
        return torchTilemap;
    }

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositionsEnumerable) {
        TileBase[] floorTilesAlternate_1 = new TileBase[] {floorTile1_1, floorTile1_2, floorTile1_3};
        TileBase[] floorTilesAlternate_2 = new TileBase[] {floorTile2_1, floorTile2_2};
        TileBase[] floorTilesAlternate_3 = new TileBase[] {floorTile3};
        TileBase[] floorTilesAlternate_4 = new TileBase[] {floorTile4};
        TileBase[] floorTilesAlternate_5 = new TileBase[] {floorTile5};

        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        floorPositions.UnionWith(floorPositionsEnumerable);

        foreach (var position in floorPositions) {
            PaintSingleTile(floorTilemap, floorTile0, position);
        }

        int state = 0;
        HashSet<Vector2Int> visited = new HashSet<Vector2Int>();
        foreach (var position in floorPositions) {
            if (visited.Contains(position)) {continue;}
            state = Random.Range(0, 100);

            if (state == 1) {
                PaintSingleTile(floorTilemap, floorTile1_1, position);
                visited.Add(position);

                Vector2Int nextTile = position + new Vector2Int(1, 0);
                if (floorPositions.Contains(nextTile)) {
                    PaintSingleTile(floorTilemap, floorTile1_2, nextTile);
                    visited.Add(nextTile);

                    Vector2Int nextNextTile = position + new Vector2Int(2, 0);
                    if (floorPositions.Contains(nextNextTile)) {
                        PaintSingleTile(floorTilemap, floorTile1_3, nextNextTile);
                        visited.Add(nextNextTile);
                    }
                
                }
                
            } else if (state == 2) {
                PaintSingleTile(floorTilemap, floorTile2_1, position);
                visited.Add(position);

                Vector2Int nextTile = position + new Vector2Int(1, 0);
                if (floorPositions.Contains(nextTile)) {
                    PaintSingleTile(floorTilemap, floorTile2_2, nextTile);
                    visited.Add(nextTile);
                }
                
            } else if (state <= 6) {
                PaintSingleTile(floorTilemap, floorTile3, position);
                visited.Add(position);
            } else if (state <= 9) {
                PaintSingleTile(floorTilemap, floorTile4, position);
                visited.Add(position);
            } else if (state == 10) {
                PaintSingleTile(floorTilemap, floorTile5, position);
                visited.Add(position);
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


    public void Clear() {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
        torchTilemap.ClearAllTiles();

        int childs = torchTilemap.transform.childCount;
        for (int i = childs - 1; i >= 0; i--) {
            GameObject.DestroyImmediate( torchTilemap.transform.GetChild( i ).gameObject );
        }

        string[] parents = new string[] {"ChestParent", "LootDropParent", "TeleporterParent"};

        foreach (string parent in parents) {
            GameObject PARENT_OBJECT = GameObject.Find(parent);
            int child = PARENT_OBJECT.transform.childCount;
            for (int i = child - 1; i >= 0; i--) {
                GameObject.DestroyImmediate( PARENT_OBJECT.transform.GetChild( i ).gameObject );
            }
        }
        
    }
}
