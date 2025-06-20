using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable))]
public class VRButtonInteractable : MonoBehaviour
{
    [Header("Quiz Settings")]
    public QuizManager quizManager;
    public int answerNumber = 1;

    [Header("Visual Feedback")]
    public Color normalColor = Color.white;
    public Color hoverColor = Color.cyan;
    public Color pressColor = Color.green;

    [Header("Audio Feedback")]
    public AudioClip hoverSound;
    public AudioClip clickSound;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable interactable;
    private Vector3 originalScale;
    private Renderer buttonRenderer;
    private AudioSource audioSource;
    private Color originalRendererColor;

    void Start()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        originalScale = transform.localScale;

        buttonRenderer = GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        if (buttonRenderer != null)
            originalRendererColor = buttonRenderer.material.color;

        OptimizeCollider();

        interactable.selectEntered.AddListener(OnSelectEntered);
        interactable.selectExited.AddListener(OnSelectExited);
        interactable.hoverEntered.AddListener(OnHoverEntered);
        interactable.hoverExited.AddListener(OnHoverExited);

        Debug.Log($"✅ VRButtonInteractable {answerNumber} initialisé sur {gameObject.name}");
    }

    void OptimizeCollider()
    {
        Collider col = GetComponent<Collider>();
        if (col == null)
        {
            BoxCollider boxCol = gameObject.AddComponent<BoxCollider>();
            boxCol.size = Vector3.one * 3f;
            Debug.Log($"📦 BoxCollider ajouté sur {gameObject.name}");
        }
        else
        {
            if (col is BoxCollider boxCollider)
            {
                boxCollider.size *= 3f;
            }
            else if (col is SphereCollider sphereCollider)
            {
                sphereCollider.radius *= 3f;
            }
            Debug.Log($"📦 Collider TRÈS agrandi sur {gameObject.name}");
        }

        if (gameObject.layer == 0)
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }

    void OnHoverEntered(HoverEnterEventArgs args)
    {
        Debug.Log($"🎯 SURVOL bouton {answerNumber} ({gameObject.name}) - Interactor: {args.interactorObject.transform.name}");

        transform.localScale = originalScale * 1.1f;
        if (buttonRenderer != null)
            buttonRenderer.material.color = hoverColor;

        if (audioSource != null && hoverSound != null)
            audioSource.PlayOneShot(hoverSound);
    }

    void OnHoverExited(HoverExitEventArgs args)
    {
        Debug.Log($"❌ SORTIE survol bouton {answerNumber} ({gameObject.name})");

        transform.localScale = originalScale;
        if (buttonRenderer != null)
            buttonRenderer.material.color = normalColor;
    }

    void OnSelectEntered(SelectEnterEventArgs args)
    {
        Debug.Log($"🔥 CLIC bouton {answerNumber} ({gameObject.name}) - Interactor: {args.interactorObject.transform.name}");

        transform.localScale = originalScale * 0.9f;
        if (buttonRenderer != null)
            buttonRenderer.material.color = pressColor;

        if (audioSource != null && clickSound != null)
            audioSource.PlayOneShot(clickSound);

        if (quizManager != null)
        {
            Debug.Log($"✅ Envoi réponse {answerNumber} au QuizManager");
            quizManager.OnAnswerSelected(answerNumber);
        }
        else
        {
            Debug.LogError($"❌ QuizManager non assigné sur {gameObject.name} !");
        }
    }

    void OnSelectExited(SelectExitEventArgs args)
    {
        Debug.Log($"🔓 RELÂCHE bouton {answerNumber} ({gameObject.name})");

        transform.localScale = originalScale * 1.1f;
        if (buttonRenderer != null)
            buttonRenderer.material.color = hoverColor;
    }

    void OnDestroy()
    {
        if (interactable != null)
        {
            interactable.selectEntered.RemoveListener(OnSelectEntered);
            interactable.selectExited.RemoveListener(OnSelectExited);
            interactable.hoverEntered.RemoveListener(OnHoverEntered);
            interactable.hoverExited.RemoveListener(OnHoverExited);
        }
    }

    [ContextMenu("Test Button Click")]
    void TestButtonClick()
    {
        Debug.Log($"🧪 Test manuel du bouton {answerNumber}");
        if (quizManager != null)
            quizManager.OnAnswerSelected(answerNumber);
    }
}