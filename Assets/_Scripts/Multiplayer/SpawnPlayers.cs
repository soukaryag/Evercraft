using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Random = UnityEngine.Random;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    private GameObject clientPlayer;

    private void Start()
    {
        Vector2 spawnLocation = new Vector2(0, Random.Range(-1, 2));

        clientPlayer = (GameObject)PhotonNetwork.Instantiate(playerPrefab.name, spawnLocation, Quaternion.identity);
        string playerName = PlayerPrefs.GetString("playerName");
        if (playerName != null) {
            clientPlayer.name = playerName;
            PhotonNetwork.NickName = playerName;
        }
        
    }
}
