using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Experimental.Rendering.Universal;
using Random = UnityEngine.Random;
using Ludiq;
using Bolt;

public class TorchPlacementGenerator
{

    [SerializeField]
    private Color torchColor = Color.yellow;
    [SerializeField]
    private Color innerTorchColor = new Color();
    [SerializeField]
    private float innerRadius = 0.1f;
    [SerializeField]
    private float outerRadius = 4f;
    [SerializeField]
    private float intensity = 0.3f;
    [SerializeField]
    private float shadowIntensity = 0.75f;
    
    private Vector2 leftOffset = new Vector2(-0.4f, 0.25f);
    private Vector2 topOffset = new Vector2(0f, 0.25f);
    private Vector2 rightOffset = new Vector2(0.4f, 0.25f);

    public void Generate(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualizer) {
        bool isTop = true;
        bool isNotTop = false;

        // LEFT
        HashSet<Vector2Int> leftTorchPositions = GenerateSide(floorPositions, new Vector2Int(-1, 0), tilemapVisualizer, leftOffset, isNotTop);
        tilemapVisualizer.PaintTorchLeft(leftTorchPositions);

        // RIGHT
        HashSet<Vector2Int> rightTorchPositions = GenerateSide(floorPositions, new Vector2Int(1, 0), tilemapVisualizer, rightOffset, isNotTop);
        tilemapVisualizer.PaintTorchRight(rightTorchPositions);

        // TOP
        // HashSet<Vector2Int> topTorchPositions = GenerateSide(floorPositions, new Vector2Int(0, 1), tilemapVisualizer, topOffset, isTop);
        // tilemapVisualizer.PaintTorchTop(topTorchPositions);
    }

    public HashSet<Vector2Int> GenerateSide(HashSet<Vector2Int> floorPositions, Vector2Int side, TilemapVisualizer tilemapVisualizer, Vector2 offset, bool isTop)
    {
        HashSet<Vector2Int> torchPositions = new HashSet<Vector2Int>();

        foreach (var position in floorPositions) {
            var neighborPosition = position + side;
            var randInt = Random.Range(0, 100);
            if (randInt < 4 && floorPositions.Contains(neighborPosition) == false) {
                if (isTop) {
                    torchPositions.Add(neighborPosition);
                } else {
                    torchPositions.Add(position);
                }
            }
        }

        int i = 0;
        UnityEngine.ColorUtility.TryParseHtmlString("#BF693A", out innerTorchColor);

        foreach (var position in torchPositions) {
            GameObject torchPlacementInner = new GameObject("torch_placement_" + i + "_0");
            torchPlacementInner.transform.parent = tilemapVisualizer.getTorchTilemap().transform;

            Light2D torchPlacementComponentInner = torchPlacementInner.AddComponent<Light2D>();
            torchPlacementComponentInner.color = innerTorchColor;
            torchPlacementComponentInner.lightType = Light2D.LightType.Point;
            torchPlacementComponentInner.pointLightOuterRadius = 1.5f;
            torchPlacementComponentInner.pointLightInnerRadius = 0.6f;
            torchPlacementComponentInner.intensity = 0.8f;
            torchPlacementComponentInner.shadowIntensity = shadowIntensity;
            torchPlacementInner.transform.position = new Vector3(position[0] + offset[0], position[1] + offset[1], 0);

            GameObject torchPlacementOuter = new GameObject("torch_placement_" + i + "_1");
            torchPlacementOuter.transform.parent = tilemapVisualizer.getTorchTilemap().transform;

            Light2D torchPlacementComponentOuter = torchPlacementOuter.AddComponent<Light2D>();
            torchPlacementComponentOuter.color = torchColor;
            torchPlacementComponentOuter.lightType = Light2D.LightType.Point;
            torchPlacementComponentOuter.pointLightOuterRadius = outerRadius;
            torchPlacementComponentOuter.pointLightInnerRadius = innerRadius;
            torchPlacementComponentOuter.intensity = intensity;
            torchPlacementComponentOuter.shadowIntensity = shadowIntensity;
            torchPlacementOuter.transform.position = new Vector3(position[0] + offset[0], position[1] + offset[1], 0);

            var flowMachine = torchPlacementComponentInner.AddComponent<FlowMachine>();
            flowMachine.nest.source = GraphSource.Macro;

            var lightFlowMacroPath = Path.Combine("Assets", "FlowMachine", "Light.asset");
            FlowMacro flowMacro = AssetDatabase.LoadAssetAtPath<FlowMacro>(lightFlowMacroPath);

            flowMachine.nest.SwitchToMacro(flowMacro);
            flowMachine.enabled = true;

            i += 1;
        }

        return torchPositions;
    }
}
