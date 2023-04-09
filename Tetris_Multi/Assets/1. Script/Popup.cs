using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Popup : MonoBehaviour
{
    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    public void Popup_SetActive()
    {
        if(SceneManager.GetActiveScene().name == "Single_GameScene")
            Time.timeScale = 0;
        this.gameObject.SetActive(true);
    }
    public void Popup_SetUnActive_time()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }

    public void Popup_SetUnActive_time_not()
    {
        Time.timeScale = 0;
        this.gameObject.SetActive(false);
    }

    public void Popoup_OutToLobby()
    {
        Time.timeScale = 1;
        PhotonNetwork.LoadLevel("In_Room");
    }

    public void LoadHomeScene()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
