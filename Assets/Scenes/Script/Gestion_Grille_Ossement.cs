using UnityEngine;

public class Gestion_Grille_Ossement : MonoBehaviour
{
    public GameObject objet; // L'objet à rendre invisible

    public void UpdateGrille(bool AsOssement)
    {
        // Vérifie la valeur de la variable
        if (AsOssement)
        {
            RendreInvisible();
        }
        else
        {
            RendreVisible();
        }
    }

    void Update()
    {
    }

    public void RendreInvisible()
    {
        objet.SetActive(false); // Désactive l'objet
        Collider collider = objet.GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false; // Désactive le collider
        }
    }

    public void RendreVisible()
    {
        objet.SetActive(true); // Active l'objet
        Collider collider = objet.GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = true; // Active le collider
        }
    }
}