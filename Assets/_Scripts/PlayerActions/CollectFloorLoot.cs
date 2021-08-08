using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectFloorLoot : MonoBehaviour
{
    [SerializeField]
    Text coinCollectedText;
    [SerializeField]
    Text interactiveText;
    [SerializeField]
    protected GameObject coinPrefab;

    private string isOnLootObject = null;

    private GameObject currentObject = null;

    private int coinCount = 0;

    private CoinSpawnGenerator coinSpawnGenerator = new CoinSpawnGenerator();

    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) {
            if (isOnLootObject == "CHEST")
            {
                coinSpawnGenerator.Generate(currentObject.transform.position, coinPrefab);

                Destroy(currentObject);
                isOnLootObject = null;
                currentObject = null;
            }
        }
        
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform.parent && collision.gameObject.transform.parent.name == "CoinParent") {
            coinCount += 1;
            coinCollectedText.text = "Coins: " + coinCount;
            Destroy(collision.gameObject);
        } else if (collision.gameObject.transform.parent && collision.gameObject.transform.parent.name == "ChestParent") {
            isOnLootObject = "CHEST";
            currentObject = collision.gameObject;

            interactiveText.text = "Press Space to Open Chest";
            interactiveText.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        interactiveText.enabled = false;
        isOnLootObject = null;
    }
}
