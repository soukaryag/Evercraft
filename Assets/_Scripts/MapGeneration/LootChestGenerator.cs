using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class LootChestGenerator : MonoBehaviour
{
    private Vector2Int Direction2dBottom = new Vector2Int(0, -1);


    public void Generate(HashSet<Vector2Int> floorPositions, GameObject chestPrefab)
    {
        HashSet<Vector2> chestPositions = new HashSet<Vector2>();

        foreach (var position in floorPositions)
        {
            var neighborPosition = position + Direction2dBottom;
            var randInt = Random.Range(0, 100);
            if (randInt < 1 && floorPositions.Contains(neighborPosition) == true) {
                chestPositions.Add((Vector2)position + new Vector2(0f, 0.5f));
            }
        }

        CreateChestSprite(chestPositions, chestPrefab);
    }

    public void CreateChestSprite(HashSet<Vector2> chestPositions, GameObject chestPrefab)
    {
        GameObject CHESTS = GameObject.Find("ChestParent");

        foreach (var position in chestPositions)
        {
            GameObject chestPlacement = Instantiate(chestPrefab, (Vector3)position, Quaternion.identity);
            chestPlacement.transform.parent = CHESTS.transform;
        }
    }
}
