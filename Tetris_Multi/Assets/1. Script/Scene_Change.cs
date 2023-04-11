using Photon.Pun;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scene_Change : MonoBehaviourPunCallbacks
{
    public void Single_Game_Scene()
    {
        SceneManager.LoadScene("Single_GameScene");
    }
    public void Multi_Game_Scene()
    {
        SceneManager.LoadScene("Multi_Room");
    }
    public void Matching_Scene()
    {
        SceneManager.LoadScene("Matching_Menu");
    }
    public void Matching_Scene_Leave_Lobby()
    {
        PhotonNetwork.LeaveLobby();
    }

    public void Main_Menu_Scene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Main_Menu_Scene_Disconnect()
    {
        SceneManager.LoadScene("MainMenu");
        PhotonNetwork.Disconnect();
    }

    public void Room_List_Scene()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Matching_Menu");
    }
    public override void OnLeftLobby()
    {
        SceneManager.LoadScene("Matching_Menu");
    }
}