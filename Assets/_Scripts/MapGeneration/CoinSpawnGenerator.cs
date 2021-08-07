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

public class CoinSpawnGenerator
{
    private Color coinGlowColor = new Color();
    private float innerRadius = 0.22f;
    private float outerRadius = 1.75f;
    private float intensity = 0.6f;
    private float shadowIntensity = 0.9f;
    private Vector3 coinSpriteScale = new Vector3(0.5f, 0.5f, 0f);


    public void Generate(HashSet<Vector2Int> floorPositions, Sprite coinSprite, RuntimeAnimatorController coinAnimation)
    {
        HashSet<Vector2Int> coinPositions = new HashSet<Vector2Int>();

        foreach (var position in floorPositions)
        {
            if (Random.Range(0, 100) < 3)
            {
                coinPositions.Add(position);
            }
        }

        CreateCoinSprite(coinPositions, coinSprite, coinAnimation);
    }

    public void CreateCoinSprite(HashSet<Vector2Int> coinPositions, Sprite coinSprite, RuntimeAnimatorController coinAnimation)
    {
        int i = 0;
        UnityEngine.ColorUtility.TryParseHtmlString("#5F89DD", out coinGlowColor);

        GameObject COINS = GameObject.Find("Coins");

        foreach (var position in coinPositions)
        {
            GameObject coinPlacement = new GameObject("coin_glow_" + i);
            coinPlacement.transform.parent = COINS.transform;

            SpriteRenderer coinPlacementSprite = coinPlacement.AddComponent<SpriteRenderer>();
            coinPlacementSprite.sprite = coinSprite;

            Light2D coinPlacementLight2d = coinPlacement.AddComponent<Light2D>();
            coinPlacementLight2d.color = coinGlowColor;
            coinPlacementLight2d.lightType = Light2D.LightType.Point;
            coinPlacementLight2d.pointLightOuterRadius = outerRadius;
            coinPlacementLight2d.pointLightInnerRadius = innerRadius;
            coinPlacementLight2d.intensity = intensity;
            coinPlacementLight2d.shadowIntensity = shadowIntensity;
            coinPlacement.transform.position = (Vector3Int)position;
            coinPlacement.transform.localScale = coinSpriteScale;

            CircleCollider2D coinPlacementCollider = coinPlacement.AddComponent<CircleCollider2D>();
            coinPlacementCollider.isTrigger = true;

            Animator coinPlacementAnimator = coinPlacement.AddComponent<Animator>();
            coinPlacementAnimator.runtimeAnimatorController = coinAnimation;

            i += 1;
        }
    }
}
