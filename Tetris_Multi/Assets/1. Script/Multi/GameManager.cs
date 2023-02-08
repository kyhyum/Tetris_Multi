using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviourPunCallbacks
{
    public TMP_Text player1;
    public TMP_Text player2;
    public static GameManager Instance;
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
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OutRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
}