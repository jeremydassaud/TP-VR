using UnityEngine;
public class Gestion_Grille_Coffre : MonoBehaviour
{
    public GameObject objet;
    public string itemID = "coffre";

    void Start()
    {
        if(PlayerPrefs.GetInt(itemID, 0) == 1) RendreInvisible();
        else RendreVisible();
    }

    void RendreInvisible()
    {
        objet.SetActive(false);
        Collider col = objet.GetComponent<Collider>();
        if(col != null) col.enabled = false;
    }

    void RendreVisible()
    {
        objet.SetActive(true);
        Collider col = objet.GetComponent<Collider>();
        if(col != null) col.enabled = true;
    }
}