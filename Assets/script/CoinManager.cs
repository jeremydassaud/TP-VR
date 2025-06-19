using UnityEngine;

// ==== GESTIONNAIRE PRINCIPAL ====
public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    [Header("État de la pièce")]
    public bool coinFound = false;
    public string coinName = "Pièce Mystérieuse";

    void Awake()
    {
        // Singleton - Un seul CoinManager
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("CoinManager créé - Persistant entre scènes");
        } else
        {
            Destroy(gameObject);
        }
    }

    // Marquer la pièce comme trouvée
    public void SetCoinFound(bool found)
    {
        coinFound = found;
        Debug.Log($"Pièce {coinName} - Trouvée: {found}");
    }

    // Vérifier si la pièce a été trouvée
    public bool IsCoinFound()
    {
        return coinFound;
    }

    // Réinitialiser (pour rejouer)
    public void ResetCoin()
    {
        coinFound = false;
        Debug.Log("État de la pièce réinitialisé");
    }
}