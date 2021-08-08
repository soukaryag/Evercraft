using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class LootChestGenerator : MonoBehaviour
{
    private Vector2Int Direction2dTop = new Vector2Int(0, 1);


    public void Generate(HashSet<Vector2Int> floorPositions, GameObject chestPrefab)
    {
        HashSet<Vector2Int> chestPositions = new HashSet<Vector2Int>();

        foreach (var position in floorPositions)
        {
            var neighborPosition = position + Direction2dTop;
            var randInt = Random.Range(0, 100);
            if (randInt < 6 && floorPositions.Contains(neighborPosition) == false) {
                chestPositions.Add(neighborPosition);
            }
        }

        CreateChestSprite(chestPositions, chestPrefab);
    }

    public void CreateChestSprite(HashSet<Vector2Int> chestPositions, GameObject chestPrefab)
    {
        GameObject CHESTS = GameObject.Find("ChestParent");

        foreach (var position in chestPositions)
        {
            GameObject chestPlacement = Instantiate(chestPrefab, (Vector3Int)position, Quaternion.identity);
            chestPlacement.transform.parent = CHESTS.transform;
        }
    }
}
