using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Random = UnityEngine.Random;

public class SpawnPlayers : MonoBehaviour
{
    public TilemapVisualizer tilemapVisualizer;
    public GameObject playerPrefab;
    public GameObject playerBasePrefab;
    private GameObject clientPlayer;
    public GameObject WorldMapGenerator;
    public int mapSize;
    public int roomSize;

    private void Start()
    {
        MapRoomGenerator mRG = WorldMapGenerator.GetComponent<MapRoomGenerator>();
        int[,] mapLayout = mRG.GenerateDungeonPublicMethod(tilemapVisualizer, mapSize, roomSize);
        Vector2 spawnLocation = new Vector2(0, 0);
        int roomMiddle = (int)(roomSize / 2);

        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                if (mapLayout[i, j] == 1)
                {
                    spawnLocation = new Vector2(i * 2 * roomSize, j * 2 * roomSize);
                    if (Random.Range(0, 10) < 3) break;
                }
            }
        }

        Debug.Log(spawnLocation);

        // spawn player
        clientPlayer = (GameObject)PhotonNetwork.Instantiate(playerPrefab.name, spawnLocation + new Vector2(1, 0), Quaternion.identity);
        string playerName = PlayerPrefs.GetString("playerName");
        if (playerName != null) {
            clientPlayer.name = playerName;
            PhotonNetwork.NickName = playerName;
        }
        
        // spawn base
        Instantiate(playerBasePrefab, spawnLocation, Quaternion.identity);
    }
}
