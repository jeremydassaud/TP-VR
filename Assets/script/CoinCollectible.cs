using UnityEngine;

public class CoinCollectible : MonoBehaviour
{
    private bool coinCollected = false;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !coinCollected)
        {
            if(Input.GetButtonDown("Grip")) // Assurez-vous de remplacer "Fire1" par le nom de l'axe de g�chette de votre manette
            {
                coinCollected = true;
                gameObject.SetActive(false); // Faites dispara�tre la pi�ce de monnaie
            }
        }
    }

    public bool IsCoinCollected()
    {
        return coinCollected;
    }
}
