using UnityEngine;

// ==== GESTIONNAIRE PRINCIPAL ====
public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    [Header("�tat de la pi�ce")]
    public bool coinFound = false;
    public string coinName = "Pi�ce Myst�rieuse";

    void Awake()
    {
        // Singleton - Un seul CoinManager
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("CoinManager cr�� - Persistant entre sc�nes");
        } else
        {
            Destroy(gameObject);
        }
    }

    // Marquer la pi�ce comme trouv�e
    public void SetCoinFound(bool found)
    {
        coinFound = found;
        Debug.Log($"Pi�ce {coinName} - Trouv�e: {found}");
    }

    // V�rifier si la pi�ce a �t� trouv�e
    public bool IsCoinFound()
    {
        return coinFound;
    }

    // R�initialiser (pour rejouer)
    public void ResetCoin()
    {
        coinFound = false;
        Debug.Log("�tat de la pi�ce r�initialis�");
    }
}