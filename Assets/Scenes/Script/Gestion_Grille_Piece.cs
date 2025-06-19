using UnityEngine;

public class Gestion_Grille_Piece : MonoBehaviour
{
    public GameObject objet; // L'objet à rendre invisible
    public bool AsPiece; // La variable qui détermine la visibilité

    void Update()
    {
        // Vérifie la valeur de la variable
        if (AsPiece)
        {
            RendreInvisible();
        }
        else
        {
            RendreVisible();
        }
    }

    void RendreInvisible()
    {
        objet.SetActive(false); // Désactive l'objet
        Collider collider = objet.GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false; // Désactive le collider
        }
    }

    void RendreVisible()
    {
        objet.SetActive(true); // Active l'objet
        Collider collider = objet.GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = true; // Active le collider
        }
    }
}