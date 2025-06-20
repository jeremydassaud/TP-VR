using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
   public void PlayGame()
   {
        SceneManager.LoadSceneAsync("GameScene"); //changer le nom avant de push
   }
}