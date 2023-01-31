using Photon.Pun;
using Photon.Pun.Demo.Cockpit;
using Photon.Realtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomManager : MonoBehaviourPunCallbacks
{

    private string userId;

    // 룸 목록 저장하기 위한 딕셔너리 자료형
    private Dictionary<string, GameObject> roomDict = new Dictionary<string, GameObject>();

    // 룸을 표시할 프리펩
    public GameObject roomPrefab;

    //Room 프리팹이 차일드화 시킬 부모 객체
    public Transform scrollContent;

    private void Awake()
    {
        // 방장이 혼자 씬을 로딩하면, 나머지 사람들은 자동으로 싱크가 됨
        PhotonNetwork.AutomaticallySyncScene = true;
        // userid 초기화
        PhotonNetwork.NickName = PlayerPrefs.GetString("NickName");
        userId = PhotonNetwork.NickName;
        Debug.Log(userId);
    }

    // 룸 생성 콜백 함수
    public override void OnCreatedRoom()
    {
        Debug.Log("방 생성 완료");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("방 입장 완료");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        GameObject tempRoom = null;
        foreach(var room in roomList)
        {
            // 룸이 삭제된 경우
            if(room.RemovedFromList == true)
            {
                roomDict.TryGetValue(room.Name, out tempRoom);
                Destroy(tempRoom);
                roomDict.Remove(room.Name);
            }
            // 룸 정보가 갱신된 경우
            else
            {
                // 룸이 처음 생성된 경우
                if(roomDict.ContainsKey(room.Name) == false) 
                {
                    GameObject _room = Instantiate(roomPrefab, scrollContent);
                    _room.GetComponent<RoomData>().roomInfo = room;
                    roomDict.Add(room.Name, _room);
                }
                // 룸 정보를 갱신하는 경우
                else
                {
                    roomDict.TryGetValue(@room.Name, out tempRoom);
                    tempRoom.GetComponent<RoomData>().roomInfo = room;
                }
            }
        }
    }

    // 룸 생성
    public void OnMakeRoomClick()
    {
        RoomOptions ro = new RoomOptions();
        ro.IsOpen= true;
        ro.IsVisible= true;
        ro.MaxPlayers = 2;

        PhotonNetwork.CreateRoom($"{userId}의 방", ro);
        // 방으로 씬 넘겨야됨
        PhotonNetwork.LoadLevel("In_Room");
    }

}
