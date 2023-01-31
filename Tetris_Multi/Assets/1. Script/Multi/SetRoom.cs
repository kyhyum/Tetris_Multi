using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetRoom : MonoBehaviourPunCallbacks
{

    private RoomInfo _roomInfo;
    public TMP_Text Room_Name_Text;

    public TMP_Text player1;
    public TMP_Text player2;

    public RoomInfo roomInfo
    {
        get { return _roomInfo; }
        set
        {
            _roomInfo = value;
            Room_Name_Text.text = _roomInfo.Name;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsConnected)
        {
            if (PhotonNetwork.PlayerList.Length > 0)
                player1.text = PhotonNetwork.PlayerList[0].ToString();
            if (PhotonNetwork.PlayerList.Length == 2)
                player2.text = PhotonNetwork.PlayerList[1].ToString();
            else
                player2.text = "none";
        }
    }
}
