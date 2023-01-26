using UnityEngine;
using UnityEngine.SceneManagement;
public class Scene_Change : MonoBehaviour
{
   public void Single_Game_Scene()
    {
        SceneManager.LoadScene("Single_GameScene");
    }
    public void Multi_Game_Scene()
    {
        SceneManager.LoadScene("Multi_GameScene");
    }
}
