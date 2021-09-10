using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    private bool connected = false;
    private bool reachedEnd = false;
    public GameObject player;

    private void Start() {
        PhotonNetwork.ConnectUsingSettings();
        player = GameObject.FindWithTag("Player");
    }

    private void Update() {
        if (player.transform.position.x >= 13) {
            reachedEnd = true;
        }

        if (connected && reachedEnd) {
            SceneManager.LoadScene("Lobby");
        }
    }

    public override void OnConnectedToMaster() {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby() {
        connected = true;
    }
}
