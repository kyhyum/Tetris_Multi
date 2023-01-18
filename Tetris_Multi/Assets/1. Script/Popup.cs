using UnityEngine;

public class Popup : MonoBehaviour
{
    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    public void Popup_SetActive()
    {
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
}
