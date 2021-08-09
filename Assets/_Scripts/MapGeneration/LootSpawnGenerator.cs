using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class LootSpawnGenerator : MonoBehaviour
{
    public void Generate(Vector3 chestPosition, GameObject coinPrefab, GameObject healthFlaskPrefab, GameObject keyPrefab)
    {
        HashSet<Vector3> coinPositions = new HashSet<Vector3>();

        for (int i = 0; i < Random.Range(2, 5); i++)
        {
            coinPositions.Add(chestPosition + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0));
        }

        CreateCoinObject(coinPositions, coinPrefab);
        if (Random.Range(0, 10) < 5) {
            CreateSpecialDropObject(chestPosition, healthFlaskPrefab);
        } else {
            CreateSpecialDropObject(chestPosition, keyPrefab);
        }
        
    }

    public void CreateCoinObject(HashSet<Vector3> coinPositions, GameObject coinPrefab)
    {
        GameObject COINS = GameObject.Find("LootDropParent");

        foreach (var position in coinPositions)
        {
            GameObject coinPlacement = Instantiate(coinPrefab, position, Quaternion.identity);
            coinPlacement.transform.parent = COINS.transform;
        }
    }

    public void CreateSpecialDropObject(Vector3 position, GameObject specialDropPrefab)
    {
        GameObject lootDropParent = GameObject.Find("LootDropParent");
        GameObject specialDropObject = Instantiate(specialDropPrefab, position, Quaternion.identity);
        specialDropObject.transform.parent = lootDropParent.transform;
    }
}
