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

    //������ ���� �Ϸ� ��
    public override void OnConnectedToMaster()
    {
        Debug.Log("���� ���� �Ϸ�");
        ConnectText.text = "Sever Connected"; 
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].interactable = true;
        }
    }

    //������ ������ �� �ٽ� ����
    public override void OnDisconnected(DisconnectCause cause)
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].interactable = false;
        }
        ConnectText.text = "Sever DisConnected";

        PhotonNetwork.ConnectUsingSettings();
    }

    //�κ� ���� ��
    public override void OnJoinedLobby()
    {
        Debug.Log($"PhotonNetwork.InLobby123 = {PhotonNetwork.InLobby}");
    }

    //���� ���� ���� ��
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 2;
        ro.IsOpen = true;
        ro.IsVisible = true;

        PhotonNetwork.CreateRoom($"{userId}�� ��", ro);
    }

    public void OnMakeRoomClick()
    {
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 2;
        ro.IsOpen = true;
        ro.IsVisible = true;

        PhotonNetwork.CreateRoom($"{userId}�� ��", ro);
        PhotonNetwork.LoadLevel("In_Room");
    }
   
    

    //�� ���� �Ϸ� ��
    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room");
        Debug.Log($"Room Name = {PhotonNetwork.CurrentRoom.Name}");
    }

    //�뿡 ���� ��
    public override void OnJoinedRoom()
    {
        Debug.Log($"Player Count = {PhotonNetwork.CurrentRoom.PlayerCount}");
        //PhotonNetwork.LoadLevel("Multi_GameScene");
    }

    //���� �� ����
    public void Random_Room_Connect()
    {
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}");
        
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

    // �� ��� ���� ����
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
