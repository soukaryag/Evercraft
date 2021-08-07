using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap, wallTilemap, transitionsTilemap, torchTilemap;
    [SerializeField]
    private TileBase floorTile, wallTop, wallSideRight, wallSideLeft, wallBottom, wallFull, 
        wallInnerCornerDownLeft, wallInnerCornerDownRight,
        wallDiagonalCornerDownLeft, wallDiagonalCornerDownRight,
        wallDiagonalCornerUpRight, wallDiagonalCornerUpLeft,
        ladderUpTile, ladderDownTile, torchLeftTile, torchTopTile, torchRightTile;

    public Tilemap getTorchTilemap() {
        return torchTilemap;
    }

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions) {
        PaintTiles(floorPositions, floorTilemap, floorTile);
    }

    public void PaintLadderUp(IEnumerable<Vector2Int> floorPositions) {
        PaintTiles(floorPositions, transitionsTilemap, ladderUpTile);
    }

    public void PaintLadderDown(IEnumerable<Vector2Int> floorPositions) {
        PaintTiles(floorPositions, transitionsTilemap, ladderDownTile);
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
        transitionsTilemap.ClearAllTiles();
        torchTilemap.ClearAllTiles();

        int childs = torchTilemap.transform.childCount;
        for (int i = childs - 1; i >= 0; i--) {
            GameObject.DestroyImmediate( torchTilemap.transform.GetChild( i ).gameObject );
        }
    }
}
