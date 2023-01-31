using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class RoomData : MonoBehaviourPunCallbacks
{
    public TMP_Text RoomInfoText;
    private RoomInfo _roomInfo;

    public RoomInfo roomInfo
    {
        get { return _roomInfo; }
        set 
        { 
            _roomInfo = value;
            RoomInfoText.text = $"{_roomInfo.Name} ({_roomInfo.PlayerCount}/{_roomInfo.MaxPlayers})";
            GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnEnterRoom(_roomInfo.Name));
        }
    }
  
    private void OnEnterRoom(string roomName)
    {
        RoomOptions ro = new RoomOptions();
        ro.IsOpen= true;
        ro.IsVisible = true;
        ro.MaxPlayers = 2;

        PhotonNetwork.JoinRoom(roomName);
        PhotonNetwork.LoadLevel("In_Room");

    }
}
