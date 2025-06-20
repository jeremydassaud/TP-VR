using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable))]
public class VRSceneTransitionButton : MonoBehaviour
{
    [Header("Scene Settings")]
    [Tooltip("Nom de la scène à charger")]
    public string targetSceneName = "";

    [Header("Timing")]
    [Tooltip("Délai avant le changement de scène")]
    public float transitionDelay = 1f;

    [Header("Visual Effects")]
    [Tooltip("Échelle lors du hover")]
    public float hoverScale = 1.1f;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable interactable;
    private Vector3 originalScale;
    private bool isTransitioning = false;

    void Start()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        originalScale = transform.localScale;

        interactable.hoverEntered.AddListener(OnHoverStart);
        interactable.hoverExited.AddListener(OnHoverEnd);

        if (string.IsNullOrEmpty(targetSceneName))
        {
            Debug.LogWarning($"Aucune scène définie pour le bouton '{gameObject.name}'");
        }
    }

    void OnHoverStart(HoverEnterEventArgs args)
    {
        if (isTransitioning) return;

        transform.localScale = originalScale * hoverScale;

        StartCoroutine(TransitionToScene());
    }

    void OnHoverEnd(HoverExitEventArgs args)
    {
        if (!isTransitioning)
        {
            transform.localScale = originalScale;
        }
    }

    IEnumerator TransitionToScene()
    {
        isTransitioning = true;

        if (string.IsNullOrEmpty(targetSceneName))
        {
            Debug.LogError("Aucune scène cible définie !");
            isTransitioning = false;
            yield break;
        }

        interactable.enabled = false;

        yield return new WaitForSeconds(transitionDelay);

        SceneManager.LoadScene(targetSceneName);
    }

    void OnDestroy()
    {
        if (interactable != null)
        {
            interactable.hoverEntered.RemoveListener(OnHoverStart);
            interactable.hoverExited.RemoveListener(OnHoverEnd);
        }
    }
}