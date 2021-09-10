using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CollectFloorLoot : MonoBehaviour
{
    [SerializeField]
    Text coinCollectedText;
    [SerializeField]
    Text interactiveText;
    [SerializeField]
    protected GameObject coinPrefab, healthFlaskPrefab, keyPrefab;
    private string isOnLootObject = null;
    private GameObject currentObject = null;
    private int coinCount = 0;
    private LootSpawnGenerator lootSpawnGenerator = new LootSpawnGenerator();
    PhotonView view;

    private void Start() {
        view = GetComponent<PhotonView>();
        coinCollectedText = GameObject.Find("CoinCount").GetComponent<Text>(); 
        interactiveText = GameObject.Find("InteractiveText").GetComponent<Text>(); 
    }

    void Update()
    {
        if (view.IsMine && Input.GetKey(KeyCode.Space)) {
            if (isOnLootObject == "CHEST")
            {
                lootSpawnGenerator.Generate(currentObject.transform.position, coinPrefab, healthFlaskPrefab, keyPrefab);

                Destroy(currentObject);
                isOnLootObject = null;
                currentObject = null;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (view.IsMine) {
            if (collision.gameObject.transform.name == "Coin(Clone)") {
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
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (view.IsMine) {
            interactiveText.enabled = false;
            isOnLootObject = null;
        }
    }
}
