using UnityEngine;

public class CollectibleCoin : MonoBehaviour
{
    [Header("Paramètres de la pièce")]
    public string coinID = "MainCoin"; // ID unique de la pièce
    public bool requiresInteraction = true; // Faut-il appuyer sur une touche?
    public KeyCode interactionKey = KeyCode.E;
    public float interactionDistance = 2f;

    [Header("Effets visuels")]
    public GameObject collectEffect; // Effet de particules
    public AudioClip collectSound; // Son de collecte

    [Header("UI")]
    public GameObject interactionPrompt; // "Appuyez sur E"

    private bool playerNearby = false;
    private Transform playerTransform;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Cacher le prompt au début
        if(interactionPrompt != null)
            interactionPrompt.SetActive(false);
    }

    void Update()
    {
        if(requiresInteraction && playerNearby)
        {
            if(Input.GetKeyDown(interactionKey))
            {
                CollectCoin();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerTransform = other.transform;
            playerNearby = true;

            if(requiresInteraction)
            {
                // Montrer le prompt d'interaction
                if(interactionPrompt != null)
                    interactionPrompt.SetActive(true);

                Debug.Log($"Appuyez sur {interactionKey} pour ramasser {coinID}");
            } else
            {
                // Collecte automatique
                CollectCoin();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerNearby = false;

            // Cacher le prompt
            if(interactionPrompt != null)
                interactionPrompt.SetActive(false);
        }
    }

    void CollectCoin()
    {
        // Marquer comme trouvée dans le manager
        if(CoinManager.Instance != null)
        {
            CoinManager.Instance.SetCoinFound(true);
        }

        // Effets visuels/sonores
        PlayCollectEffects();

        // Faire disparaître la pièce
        gameObject.SetActive(false);

        Debug.Log($"Pièce {coinID} collectée!");
    }

    void PlayCollectEffects()
    {
        // Effet de particules
        if(collectEffect != null)
        {
            Instantiate(collectEffect, transform.position, transform.rotation);
        }

        // Son
        if(collectSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(collectSound);
        }
    }
}