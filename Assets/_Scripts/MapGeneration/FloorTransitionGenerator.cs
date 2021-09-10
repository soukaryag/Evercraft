using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class FloorTransitionGenerator : MonoBehaviour
{
    public void CreateTeleporterOut(Vector2Int teleporterPosition, GameObject teleporterPrefab, GameObject teleporterInPrefab) {
        GameObject TELEPORTER = GameObject.Find("TeleporterParent");

        GameObject teleporterPlacement = Instantiate(teleporterPrefab, (Vector3Int)teleporterPosition, Quaternion.identity);
        teleporterPlacement.transform.parent = TELEPORTER.transform;

        GameObject teleporterInPlacement = Instantiate(teleporterInPrefab, new Vector3Int(0, 0, 0), Quaternion.identity);
        teleporterInPlacement.transform.parent = TELEPORTER.transform;
    }

}
