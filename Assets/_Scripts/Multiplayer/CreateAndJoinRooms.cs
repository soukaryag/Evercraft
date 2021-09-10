using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{

    public InputField createInput;
    public InputField joinInput;
    public InputField playerName;

    public void CreateRoom() {
        PhotonNetwork.CreateRoom(createInput.text);
    }

    public void JoinRoom() {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom() {
        PlayerPrefs.SetString("playerName", playerName.text);
        PhotonNetwork.LoadLevel("Game");
    }
}
