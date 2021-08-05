using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FloorTransitionGenerator
{
    public static void CreateLadderDown(Vector2Int ladderPosition, TilemapVisualizer tilemapVisualizer) {
        HashSet<Vector2Int> ladderPositions = new HashSet<Vector2Int>();
        ladderPositions.Add(ladderPosition);
        
        tilemapVisualizer.PaintLadderDown(ladderPositions);
    }

}
