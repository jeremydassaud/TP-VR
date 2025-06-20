using UnityEngine;
using System.Collections;

public class SuccessAnimationManager : MonoBehaviour
{
    [Header("Visual Effects")]
    public ParticleSystem celebrationParticles;
    public GameObject successUI;
    public Light celebrationLight;

    [Header("Audio")]
    public AudioClip successSound;
    public AudioClip fanfareSound;
    private AudioSource audioSource;

    [Header("Animation Settings")]
    public float animationDuration = 2f;
    public AnimationCurve scaleCurve = AnimationCurve.EaseInOut(0, 0.5f, 1, 1.2f);
    public AnimationCurve lightIntensityCurve = AnimationCurve.EaseInOut(0, 0, 0.5f, 2f);
    public Color successColor = Color.green;

    [Header("Screen Effects")]
    public bool enableScreenFlash = true;
    public Color flashColor = new Color(1, 1, 1, 0.3f);

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        if (successUI != null)
            successUI.SetActive(false);
    }

    public void PlaySuccessAnimation()
    {
        StartCoroutine(SuccessAnimationSequence());
    }

    IEnumerator SuccessAnimationSequence()
    {
        Debug.Log("🎉 Animation de succès lancée !");

        if (enableScreenFlash)
            StartCoroutine(ScreenFlash());

        if (audioSource != null && successSound != null)
            audioSource.PlayOneShot(successSound);

        if (successUI != null)
        {
            successUI.SetActive(true);
            StartCoroutine(AnimateSuccessUI());
        }

        if (celebrationParticles != null)
        {
            celebrationParticles.Play();
        }

        if (celebrationLight != null)
        {
            StartCoroutine(AnimateLight());
        }

        yield return new WaitForSeconds(0.5f);
        if (audioSource != null && fanfareSound != null)
            audioSource.PlayOneShot(fanfareSound);

        yield return new WaitForSeconds(animationDuration - 0.5f);

        if (successUI != null)
            successUI.SetActive(false);

        Debug.Log("✅ Animation de succès terminée !");
    }

    IEnumerator ScreenFlash()
    {
        GameObject flashOverlay = new GameObject("FlashOverlay");
        Canvas canvas = flashOverlay.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 1000;

        UnityEngine.UI.Image flashImage = flashOverlay.AddComponent<UnityEngine.UI.Image>();
        flashImage.color = flashColor;
        flashImage.raycastTarget = false;

        float elapsed = 0f;
        float duration = 0.3f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(flashColor.a, 0f, elapsed / duration);
            Color currentColor = flashColor;
            currentColor.a = alpha;
            flashImage.color = currentColor;
            yield return null;
        }

        Destroy(flashOverlay);
    }

    IEnumerator AnimateSuccessUI()
    {
        float elapsed = 0f;
        Vector3 originalScale = successUI.transform.localScale;

        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / animationDuration;

            float scaleMultiplier = scaleCurve.Evaluate(progress);
            successUI.transform.localScale = originalScale * scaleMultiplier;

            yield return null;
        }

        successUI.transform.localScale = originalScale;
    }

    IEnumerator AnimateLight()
    {
        Color originalColor = celebrationLight.color;
        float originalIntensity = celebrationLight.intensity;

        float elapsed = 0f;

        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / animationDuration;

            celebrationLight.color = Color.Lerp(originalColor, successColor, Mathf.Sin(progress * Mathf.PI));
            celebrationLight.intensity = originalIntensity + lightIntensityCurve.Evaluate(progress);

            yield return null;
        }

        celebrationLight.color = originalColor;
        celebrationLight.intensity = originalIntensity;
    }

    [ContextMenu("Test Success Animation")]
    public void TestAnimation()
    {
        PlaySuccessAnimation();
    }
}