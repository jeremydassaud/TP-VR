using UnityEngine;

public class Gestion_Grille_Piece : MonoBehaviour
{
    public GameObject objet; // L'objet � rendre invisible

    public void UpdateGrille(bool AsPiece)
    {
        // V�rifie la valeur de la variable
        if (AsPiece)
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
        objet.SetActive(false); // D�sactive l'objet
        Collider collider = objet.GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false; // D�sactive le collider
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