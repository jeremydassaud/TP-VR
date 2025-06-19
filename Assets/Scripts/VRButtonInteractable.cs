using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable))]
public class VRButtonInteractable : MonoBehaviour
{
    [Header("Quiz Settings")]
    public QuizManager quizManager;
    public int answerNumber = 1;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable interactable;
    private Vector3 originalScale;

    void Start()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        originalScale = transform.localScale;

        interactable.selectEntered.AddListener(OnSelectEntered);
        interactable.selectExited.AddListener(OnSelectExited);
        interactable.hoverEntered.AddListener(OnHoverEntered);
        interactable.hoverExited.AddListener(OnHoverExited);
    }

    void OnSelectEntered(SelectEnterEventArgs args)
    {
        transform.localScale = originalScale * 0.9f;

        if (quizManager != null)
        {
            quizManager.OnAnswerSelected(answerNumber);
        }
    }

    void OnSelectExited(SelectExitEventArgs args)
    {
        transform.localScale = originalScale;
    }

    void OnHoverEntered(HoverEnterEventArgs args)
    {
        transform.localScale = originalScale * 1.1f;
    }

    void OnHoverExited(HoverExitEventArgs args)
    {
        transform.localScale = originalScale;
    }
}