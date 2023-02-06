using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

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
            this.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnEnterRoom(_roomInfo.Name));
        }
    }

    private void OnEnterRoom(string roomName)
    {
        Debug.Log(roomName + "dd");
        PhotonNetwork.JoinRoom(roomName);
        PhotonNetwork.LoadLevel("In_Room");
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("tq");
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("방 입장 완료");
    }
   
}