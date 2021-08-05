using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PlayerMapInteraction : MonoBehaviour
{

    [SerializeField]
    Text interactiveText;

    bool isOnObject = false;

    [SerializeField]
    int currentFloor = 0;
    [SerializeField]
    int totalFloors = 3;
    [SerializeField]
    int floorSpacing = 50;


    void Update()
    {
        if (isOnObject && Input.GetKey(KeyCode.Space))
        {
            var playerObject = GameObject.Find("Player");
            currentFloor = (currentFloor + 1) % totalFloors;
            playerObject.transform.position = new Vector3((currentFloor * floorSpacing) % (totalFloors * floorSpacing), 0f, 0f);
            isOnObject = false;

            Debug.Log("currentFloor: " + currentFloor);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "FloorDown") {
            interactiveText.text = "Press Space to Descend";
        } else if (collision.gameObject.name == "FloorUp") {
            interactiveText.text = "Press Space to Ascend";
        } else if (collision.gameObject.name == "Treasure") {
            interactiveText.text = "Press Space to Open";
        } else if (collision.gameObject.name == "HealthPickup") {
            interactiveText.text = "Press Space to Consume";
        } else if (collision.gameObject.name == "WeaponPickup") {
            interactiveText.text = "Press Space to Equip";
        }
        
        interactiveText.enabled = true;
        isOnObject = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        interactiveText.enabled = false;
        isOnObject = false;
    }

}
