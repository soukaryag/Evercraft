using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Experimental.Rendering.Universal;
using Random = UnityEngine.Random;

public class TorchPlacementGenerator
{

    [SerializeField]
    private Color torchColor = Color.yellow;
    [SerializeField]
    private float innerRadius = 0.2f;
    [SerializeField]
    private float outerRadius = 1.5f;
    [SerializeField]
    private float intensity = 0.4f;
    [SerializeField]
    private float shadowIntensity = 0.75f;

    public void Generate(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList, TilemapVisualizer tilemapVisualizer)
    {
        HashSet<Vector2Int> torchPositions = new HashSet<Vector2Int>();
        foreach (var position in floorPositions) {
            var neighborPosition = position + new Vector2Int(-1, 0);
            if (Random.Range(0, 100) < 10 && floorPositions.Contains(neighborPosition) == false) {
                torchPositions.Add(position);
            }
        }

        tilemapVisualizer.PaintTorch(torchPositions);
        int i = 0;

        foreach (var position in torchPositions) {
            GameObject torchPlacement = new GameObject("torch_placement_" + i);
            torchPlacement.transform.parent = tilemapVisualizer.getTorchTilemap().transform;
            Light2D torchPlacementComponent = torchPlacement.AddComponent<Light2D>();
            torchPlacementComponent.color = torchColor;
            torchPlacementComponent.lightType = Light2D.LightType.Point;
            torchPlacementComponent.pointLightOuterRadius = outerRadius;
            torchPlacementComponent.pointLightInnerRadius = innerRadius;
            torchPlacementComponent.intensity = intensity;
            torchPlacementComponent.shadowIntensity = shadowIntensity;
            torchPlacement.transform.position = (Vector3Int)position;

            i += 1;
        }

        Debug.Log("Generate");
    }

    // public void DestroyAllChildren()
    // {

        // var tempList = transform.Cast<Transform>().ToList();
        // foreach (var child in tempList)
        // {
        //     DestroyImmediate(child.gameObject);
        // }

        // Tilemap current = GetComponent<Tilemap>();
        // current.ClearAllTiles();

    // }
}
