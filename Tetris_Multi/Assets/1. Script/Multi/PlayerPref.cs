using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerPref : MonoBehaviour
{
    public TMP_InputField inputName;
    public TMP_Text text;

    private string guestId;
    public void Awake()
    {
        guestId = "guest" + Random.Range(10000, 99999).ToString();
        PlayerPrefs.SetString("NickName", guestId);
        PhotonNetwork.NickName = (PlayerPrefs.GetString("NickName") == null) ? guestId : PlayerPrefs.GetString("NickName");
    }
    public void Save()
    {
        PlayerPrefs.SetString("NickName",inputName.text);
        PhotonNetwork.NickName = PlayerPrefs.GetString("NickName");
    }
}
