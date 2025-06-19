using UnityEngine;

public class CollectibleCoin : MonoBehaviour
{
    [Header("Param�tres de la pi�ce")]
    public string coinID = "MainCoin"; // ID unique de la pi�ce
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

        // Cacher le prompt au d�but
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
        // Marquer comme trouv�e dans le manager
        if(CoinManager.Instance != null)
        {
            CoinManager.Instance.SetCoinFound(true);
        }

        // Effets visuels/sonores
        PlayCollectEffects();

        // Faire dispara�tre la pi�ce
        gameObject.SetActive(false);

        Debug.Log($"Pi�ce {coinID} collect�e!");
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