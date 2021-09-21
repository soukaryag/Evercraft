using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerMapInteraction : MonoBehaviour
{

    [SerializeField]
    Text interactiveText;

    int roomTransferCooldown = 0;
    private int roomSize = 30;
    private int buffer = 3;
    PhotonView view;

    private CorridorFirstDungeonGenerator corridorFirstDungeonGenerator;

    void Awake()
    {
        // this.corridorFirstDungeonGenerator = new CorridorFirstDungeonGenerator();
        view = GetComponent<PhotonView>();
        interactiveText = GameObject.Find("InteractiveText").GetComponent<Text>();
    }


    void Update()
    {
        if (roomTransferCooldown > 0) {
            roomTransferCooldown--;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (view.IsMine)
        {
            if (collision.gameObject.name.Contains("Exit") && roomTransferCooldown == 0)
            {
                string[] exitParams = collision.gameObject.name.Split(',');
                int yRoomIndex = Mathf.RoundToInt(Int32.Parse(exitParams[2]) / roomSize);
                int xRoomIndex = Mathf.RoundToInt(Int32.Parse(exitParams[1]) / roomSize);

                if (exitParams[3] == "up") {
                    transform.position = transform.position + (Vector3)(new Vector2(0, roomSize + buffer));
                } else if (exitParams[3] == "down") {
                    transform.position = transform.position + (Vector3)(new Vector2(0, -1*(roomSize + buffer)));
                } else if (exitParams[3] == "right") {
                    transform.position = transform.position + (Vector3)(new Vector2(roomSize + buffer, 0));
                } else if (exitParams[3] == "left") {
                    transform.position = transform.position + (Vector3)(new Vector2(-1*(roomSize + buffer), 0));
                }

                roomTransferCooldown = 30;
                return;
            } else if (collision.gameObject.name.Contains("Shrub")) {
                GameObject.Destroy(collision.gameObject);
            }

            interactiveText.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (view.IsMine)
        {
            interactiveText.enabled = false;
        }
    }

}
