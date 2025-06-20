using UnityEngine;

public class Gestion_Grille_Coffre : MonoBehaviour
{
    public GameObject objet; // L'objet � rendre invisible

    public void UpdateGrille(bool AsCoffre)
    {
        // V�rifie la valeur de la variable
        if (AsCoffre)
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