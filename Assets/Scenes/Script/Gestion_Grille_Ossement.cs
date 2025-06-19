using UnityEngine;

public class Gestion_Grille_Ossement : MonoBehaviour
{
    public GameObject objet; // L'objet � rendre invisible
    public bool AsOssement; // La variable qui d�termine la visibilit�

    void Update()
    {
        // V�rifie la valeur de la variable
        if (AsOssement)
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
        objet.SetActive(false); // D�sactive l'objet
        Collider collider = objet.GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false; // D�sactive le collider
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