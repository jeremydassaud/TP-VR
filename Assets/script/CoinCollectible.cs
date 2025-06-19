using UnityEngine;

public class CoinCollectible : MonoBehaviour
{
    private bool coinCollected = false;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !coinCollected)
        {
            if(Input.GetButtonDown("Grip")) // Assurez-vous de remplacer "Fire1" par le nom de l'axe de gâchette de votre manette
            {
                coinCollected = true;
                gameObject.SetActive(false); // Faites disparaître la pièce de monnaie
            }
        }
    }

    public bool IsCoinCollected()
    {
        return coinCollected;
    }
}
