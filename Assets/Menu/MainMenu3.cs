using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager3 : MonoBehaviour
{
   public void QuitGame()
   {
       Application.Quit();
       Debug.Log("Quitter le jeu");
   }
}