using UnityEngine;
using UnityEngine.SceneManagement;

public class PressurePlateManager : MonoBehaviour
{
    public int totalPlates = 3; // Nombre total de plaques
    private int activatedPlates = 0; // Compteur de plaques activées

    public void ActivatePlate()
    {
        activatedPlates++;
        if (activatedPlates >= totalPlates)
        {
            Teleporter();
        }
    }

    void Teleporter()
    {
        SceneManager.LoadScene("EcranFin"); // Remplacez par le nom de la scène de destination
    }
}