using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class SeverManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI ConnectText;
    public Button[] Buttons;
    private readonly string version = "1.0f";
    private string userId;

    public InputField playerNameInput;

    private void Awake()
    {
        Debug.Log("nickanme : "+PhotonNetwork.NickName);
        userId = PhotonNetwork.NickName;

        PhotonNetwork.AutomaticallySyncScene = true;

        PhotonNetwork.GameVersion = version;

        Debug.Log(PhotonNetwork.SendRate);

        PhotonNetwork.ConnectUsingSettings();
        for(int i = 0; i<Buttons.Length; i++)
        {
            Buttons[i].interactable = false;
        }
    }

    //서버에 연결 완료 후
    public override void OnConnectedToMaster()
    {
        Debug.Log("서버 연결 완료");
        ConnectText.text = "Sever Connected"; 
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].interactable = true;
        }
    }

    //서버가 끊겼을 때 다시 접속
    public override void OnDisconnected(DisconnectCause cause)
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].interactable = false;
        }
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

    public void OnMakeRoomClick()
    {
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 2;
        ro.IsOpen = true;
        ro.IsVisible = true;

        PhotonNetwork.CreateRoom($"{userId}의 방", ro);
        PhotonNetwork.LoadLevel("In_Room");
    }
   
    

    //룸 생성 완료 후
    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room");
        Debug.Log($"Room Name = {PhotonNetwork.CurrentRoom.Name}");
    }

    //룸에 입장 후
    public override void OnJoinedRoom()
    {
        Debug.Log($"Player Count = {PhotonNetwork.CurrentRoom.PlayerCount}");
        //PhotonNetwork.LoadLevel("Multi_GameScene");
    }

    //랜덤 룸 접속
    public void Random_Room_Connect()
    {
        PhotonNetwork.JoinLobby();
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}");
        PhotonNetwork.ConnectUsingSettings();
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].interactable = false;
        }
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

    // 방 목록 씬에 접속
    public void RoomList_Connect()
    {
        PhotonNetwork.JoinLobby();
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}");
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].interactable = false;
        }
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.LoadLevel("Multi_Room");
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }
}
