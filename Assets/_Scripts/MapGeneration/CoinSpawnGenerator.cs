using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinSpawnGenerator : MonoBehaviour
{
    public void Generate(Vector3 chestPosition, GameObject coinPrefab)
    {
        HashSet<Vector3> coinPositions = new HashSet<Vector3>();

        for (int i = 0; i < Random.Range(1, 10); i++)
        {
            coinPositions.Add(chestPosition + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 0f), 0));
        }

        CreateCoinSprite(coinPositions, coinPrefab);
    }

    public void CreateCoinSprite(HashSet<Vector3> coinPositions, GameObject coinPrefab)
    {
        GameObject COINS = GameObject.Find("CoinParent");

        foreach (var position in coinPositions)
        {
            GameObject coinPlacement = Instantiate(coinPrefab, position, Quaternion.identity);
            coinPlacement.transform.parent = COINS.transform;
        }
    }
}
