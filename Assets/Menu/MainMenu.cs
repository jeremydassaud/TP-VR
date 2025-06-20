using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
   public void PlayGame()
   {
        SceneManager.LoadSceneAsync("Salle1"); //changer le nom avant de push
   }
}