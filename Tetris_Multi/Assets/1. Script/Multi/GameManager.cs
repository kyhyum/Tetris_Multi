using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance;
    // Start is called before the first frame update
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {

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