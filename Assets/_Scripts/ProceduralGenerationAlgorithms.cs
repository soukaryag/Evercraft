using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class ProceduralGenerationAlgorithms
{
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength) {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add(startPosition);
        var previousPosition = startPosition;

        for (int i = 0; i < walkLength; i++) {
            var newPosition = previousPosition + Direction2D.GetRandomCardinalDireciton();
            path.Add(newPosition);
            previousPosition = newPosition;
        }
        
        return path;
    }

    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorlength) {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = Direction2D.GetRandomCardinalDireciton();
        var currentPosition = startPosition;
        corridor.Add(currentPosition);

        for (int i = 0; i < corridorlength; i ++) {
            currentPosition += direction;
            corridor.Add(currentPosition);
        }

        return corridor;
    }
}

public static class Direction2D {
    public static List<Vector2Int> cardinalDirectionList = new List<Vector2Int> {
        new Vector2Int(0, 1), // UP
        new Vector2Int(1, 0), // RIGHT
        new Vector2Int(0, -1), // DOWN
        new Vector2Int(-1, 0) // LEFT
    };

    public static List<Vector2Int> diagonalDirectionList = new List<Vector2Int> {
        new Vector2Int(1, 1), // UP-RIGHT
        new Vector2Int(1, -1), // RIGHT-DOWN
        new Vector2Int(1, -1), // DOWN-LEFT
        new Vector2Int(-1, 1) // LEFT-UP
    };

    public static List<Vector2Int> eightDirectionList = new List<Vector2Int> {
        new Vector2Int(0, 1), // UP
        new Vector2Int(1, 1), // UP-RIGHT
        new Vector2Int(1, 0), // RIGHT
        new Vector2Int(1, -1), // RIGHT-DOWN
        new Vector2Int(0, -1), // DOWN
        new Vector2Int(1, -1), // DOWN-LEFT
        new Vector2Int(-1, 0), // LEFT
        new Vector2Int(-1, 1) // LEFT-UP
    };

    public static Vector2Int GetRandomCardinalDireciton() {
        return cardinalDirectionList[Random.Range(0, cardinalDirectionList.Count)];
    }
}
