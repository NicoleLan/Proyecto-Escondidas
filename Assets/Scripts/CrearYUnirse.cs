using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class CrearYUnirse : MonoBehaviourPunCallbacks
{
    public TMP_InputField InputCrear;
    public TMP_InputField InputUnirse;

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(InputCrear.text, new RoomOptions() { MaxPlayers = 8, IsVisible = true, IsOpen = true }, TypedLobby.Default, null);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(InputUnirse.text);
    }


    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Sala");
        print(PhotonNetwork.CountOfPlayersInRooms);
    }
}
