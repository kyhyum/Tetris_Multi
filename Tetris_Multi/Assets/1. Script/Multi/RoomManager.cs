using Photon.Pun;
using Photon.Pun.Demo.Cockpit;
using Photon.Realtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public Button btn;
    private string userId;

    // �� ��� �����ϱ� ���� ��ųʸ� �ڷ���
    private Dictionary<string, GameObject> roomDict = new Dictionary<string, GameObject>();

    // ���� ǥ���� ������
    public GameObject roomPrefab;

    //Room �������� ���ϵ�ȭ ��ų �θ� ��ü
    public Transform scrollContent;

    private void Awake()
    {
        // ������ ȥ�� ���� �ε��ϸ�, ������ ������� �ڵ����� ��ũ�� ��
        //PhotonNetwork.AutomaticallySyncScene = true;
        // userid �ʱ�ȭ
        PhotonNetwork.NickName = PlayerPrefs.GetString("NickName");
        userId = PhotonNetwork.NickName;
        Debug.Log(userId);
    }
    // �� ���� �ݹ� �Լ�
    public override void OnCreatedRoom()
    {
        Debug.Log("�� ���� �Ϸ�");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("�� ���� �Ϸ�");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        GameObject tempRoom = null;
        foreach(var room in roomList)
        {
            // ���� ������ ���
            if(room.RemovedFromList == true)
            {
                roomDict.TryGetValue(room.Name, out tempRoom);
                Destroy(tempRoom);
                roomDict.Remove(room.Name);
            }
            // �� ������ ���ŵ� ���
            else
            {
                // ���� ó�� ������ ���
                if(roomDict.ContainsKey(room.Name) == false) 
                {
                    GameObject _room = Instantiate(roomPrefab, scrollContent);
                    _room.GetComponent<RoomData>().roomInfo = room;
                    roomDict.Add(room.Name, _room);
                }
                // �� ������ �����ϴ� ���
                else
                {
                    roomDict.TryGetValue(@room.Name, out tempRoom);
                    tempRoom.GetComponent<RoomData>().roomInfo = room;
                }
            }
        }
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 2;
        ro.IsOpen = true;
        ro.IsVisible = true;

        PhotonNetwork.CreateRoom($"{userId}�� ��", ro);
    }

    // �� ����
    public void OnMakeRoomClick()
    {
        RoomOptions ro = new RoomOptions();
        ro.IsOpen= true;
        ro.IsVisible= true;
        ro.MaxPlayers = 2;

        PhotonNetwork.CreateRoom($"{userId}�� ��", ro);
        // ������ �� �Ѱܾߵ�
        PhotonNetwork.LoadLevel("In_Room");
    }

}
