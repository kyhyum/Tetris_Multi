using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class SeverManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI ConnectText;
    public Button serverbtn;
    private readonly string version = "1.0f";
    private string userId;

    private void Awake()
    {
        userId = "guest" + Random.Range(10000, 99999).ToString();
        PhotonNetwork.AutomaticallySyncScene = true;

        PhotonNetwork.GameVersion = version;
        PhotonNetwork.NickName = userId;

        Debug.Log(PhotonNetwork.SendRate);

        PhotonNetwork.ConnectUsingSettings();
        serverbtn.interactable = false;
    }

    //서버에 연결 완료 후
    public override void OnConnectedToMaster()
    {
        Debug.Log("서버 연결 완료");
        ConnectText.text = "Sever Connected";
        serverbtn.interactable = true;
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}");
        PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        serverbtn.interactable = false;
        ConnectText.text = "Sever DisConnected";

        PhotonNetwork.ConnectUsingSettings();
    }

    //로비 입장 후
    public override void OnJoinedLobby()
    {
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}");
    }

    //랜덤 입장 실패 시
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 2;
        ro.IsOpen = true;
        ro.IsVisible = true;

        PhotonNetwork.CreateRoom($"{userId}의 방", ro);
    }

    //룸 생선 완료 후
    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room");
        Debug.Log($"Room Name = {PhotonNetwork.CurrentRoom.Name}");
    }

    //룸에 입장 후
    public override void OnJoinedRoom()
    {
        Debug.Log($"Player Count = {PhotonNetwork.CurrentRoom.PlayerCount}");
        PhotonNetwork.LoadLevel("Multi_GameScene");
    }

    public void Connect()
    {
        serverbtn.interactable = false;
        if (PhotonNetwork.IsConnected)
        {
            ConnectText.text = "Connecting to Random Room...";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
}
