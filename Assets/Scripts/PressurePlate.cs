using UnityEngine;
using UnityEngine.SceneManagement;

public class PressurePlate : MonoBehaviour
{
    [Header("Pressure Plate Settings")]
    public bool isCorrectPlate = false;
    public Transform teleportDestination;
    public Transform playerTransform;

    [Header("Scene Management")]
    public bool isLastPressurePlate = false;
    public string nextSceneName = "";

    [Header("Visual Feedback")]
    public Material normalMaterial;
    public Material activatedMaterial;
    public Material wrongMaterial;
    public AudioClip correctSound;
    public AudioClip wrongSound;

    [Header("Animation")]
    public float pressDepth = 0.2f;
    public float animationSpeed = 10f;
    public float resetDelay = 2f;

    [Header("Success Animation")]
    public SuccessAnimationManager successAnimationManager;
    public ParticleSystem plateSuccessParticles;

    private Renderer plateRenderer;
    private AudioSource audioSource;
    private Vector3 originalPosition;
    private Vector3 pressedPosition;
    private bool isPressed = false;
    private bool isActivated = false;

    void Start()
    {
        plateRenderer = GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>();

        originalPosition = transform.localPosition;
        pressedPosition = originalPosition - Vector3.up * pressDepth;

        if (plateRenderer != null && normalMaterial != null)
            plateRenderer.material = normalMaterial;
    }

    void Update()
    {
        Vector3 targetPosition = isPressed ? pressedPosition : originalPosition;
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * animationSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (IsPlayer(other) && !isActivated)
        {
            ActivatePlate();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (IsPlayer(other) && !isActivated)
        {
            isPressed = false;
        }
    }

    bool IsPlayer(Collider other)
    {
        return other.CompareTag("Player") ||
               other.transform.root == playerTransform ||
               other.name.Contains("XR") ||
               other.name.Contains("Player");
    }

    void ActivatePlate()
    {
        isPressed = true;
        isActivated = true;

        if (isCorrectPlate)
        {
            Debug.Log("Bonne plaque ! Téléportation...");

            if (plateRenderer != null && activatedMaterial != null)
                plateRenderer.material = activatedMaterial;

            if (audioSource != null && correctSound != null)
                audioSource.PlayOneShot(correctSound);

            if (successAnimationManager != null)
            {
                successAnimationManager.PlaySuccessAnimation();
            }

            if (plateSuccessParticles != null)
            {
                plateSuccessParticles.Play();
            }

            Invoke("TeleportPlayer", 2.5f);
        }
        else
        {
            Debug.Log("Mauvaise plaque ! Essayez encore.");

            if (plateRenderer != null && wrongMaterial != null)
                plateRenderer.material = wrongMaterial;

            if (audioSource != null && wrongSound != null)
                audioSource.PlayOneShot(wrongSound);

            Invoke("ResetPlate", resetDelay);
        }
    }

    void TeleportPlayer()
    {
        Debug.Log($"=== TELEPORTATION DEBUG ===");
        Debug.Log($"Player Transform: {(playerTransform != null ? playerTransform.name : "NULL")}");
        Debug.Log($"Teleport Destination: {(teleportDestination != null ? teleportDestination.name : "NULL")}");
        Debug.Log($"Is Last Pressure Plate: {isLastPressurePlate}");
        Debug.Log($"Next Scene Name: {nextSceneName}");

        if (isLastPressurePlate && !string.IsNullOrEmpty(nextSceneName))
        {
            Debug.Log($"Dernière plaque activée ! Changement de scène vers '{nextSceneName}' dans 2 secondes...");
            Invoke("ChangeScene", 2f);
        }
        else if (playerTransform != null && teleportDestination != null)
        {
            Debug.Log($"Destination Position: {teleportDestination.position}");
            Debug.Log($"Player Position AVANT: {playerTransform.position}");

            playerTransform.position = teleportDestination.position;
            playerTransform.rotation = teleportDestination.rotation;
            Debug.Log($"Player Position APRÈS: {playerTransform.position}");
            Debug.Log("Joueur téléporté vers la prochaine salle !");
        }
        else
        {
            Debug.LogWarning("PlayerTransform ou TeleportDestination non assigné !");
        }
        Debug.Log($"============================");
    }

    void ChangeScene()
    {
        Debug.Log($"Changement de scène vers: {nextSceneName}");
        SceneManager.LoadScene(nextSceneName);
    }

    void ResetPlate()
    {
        isPressed = false;
        isActivated = false;

        if (plateRenderer != null && normalMaterial != null)
            plateRenderer.material = normalMaterial;
    }

    void OnDrawGizmosSelected()
    {
        if (isCorrectPlate)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position + Vector3.up * 0.5f, new Vector3(2f, 1f, 2f));
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + Vector3.up * 0.5f, new Vector3(2f, 1f, 2f));
        }
    }
}