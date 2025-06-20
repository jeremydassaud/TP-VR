using UnityEngine;
public class Gestion_Grille_Piece : MonoBehaviour
{
    public GameObject objet;     // La porte ou grille � cacher
    public string coinID = "coin1"; // M�me ID que celui dans CoinCollectible

    void Start()
    {
        if(PlayerPrefs.GetInt(coinID, 0) == 1)
        {
            RendreInvisible();
        } else
        {
            RendreVisible();
        }
    }

    public void RendreInvisible()
    {
        objet.SetActive(false);
        Collider collider = objet.GetComponent<Collider>();
        if(collider != null)
            collider.enabled = false;
    }

    public void RendreVisible()
    {
        objet.SetActive(true);
        Collider collider = objet.GetComponent<Collider>();
        if(collider != null)
            collider.enabled = true;
    }
}