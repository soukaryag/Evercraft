using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectFloorLoot : MonoBehaviour
{
    [SerializeField]
    Text coinCollectedText;

    private int coinCount = 0;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform.parent && collision.gameObject.transform.parent.name == "Coins") {
            coinCount += 1;
            coinCollectedText.text = "Coins: " + coinCount;

            Destroy(collision.gameObject);
        }
    }
}
