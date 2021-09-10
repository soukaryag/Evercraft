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

    bool isOnTeleporter = false;

    [SerializeField]
    int currentFloor = 0;
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
        if (view.IsMine && isOnTeleporter && Input.GetKey(KeyCode.E))
        {
            currentFloor++;
            // this.corridorFirstDungeonGenerator.CreateNextFloor();
            this.transform.position = new Vector3(0f, 0f, 0f);
            isOnTeleporter = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (view.IsMine)
        {
            if (collision.gameObject.name == "Teleporter(Clone)")
            {
                interactiveText.text = "Press E to Ascend";
                isOnTeleporter = true;
            }
            else
            {
                return;
            }

            interactiveText.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (view.IsMine)
        {
            interactiveText.enabled = false;
            isOnTeleporter = false;
        }
    }

}
