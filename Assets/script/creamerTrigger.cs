using System.Collections;
using UnityEngine;

public class ScreamerTrigger : MonoBehaviour
{
    public GameObject screamerCanvas;
    public Transform player;
    public float screamerDuration = 2.5f;
    public float invincibilityDuration = 3f;

    private bool isPlayerInvincible = false;
    private bool hasTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered && !isPlayerInvincible)
        {
            hasTriggered = true;
            StartCoroutine(HandleScreamer());
        }
    }

    IEnumerator HandleScreamer()
    {
        // 1. Affiche le screamer
        screamerCanvas.SetActive(true);

        // 2. Stop vision et poursuite du joueur
        var vision = GetComponent<MonsterVision>();
        vision.ForceStopChase();
        vision.enabled = false;

        // 3. Patrouille reprend
        var patrol = GetComponent<MonsterPatrol>();
        patrol.enabled = true;
        patrol.ChooseNextPoint();

        // 4. Attend la durée du screamer
        yield return new WaitForSeconds(screamerDuration);

        // 5. Cache le screamer
        screamerCanvas.SetActive(false);

        // 6. Attente d'invincibilité avant réactivation vision
        isPlayerInvincible = true;
        yield return new WaitForSeconds(invincibilityDuration);

        vision.enabled = true;
        isPlayerInvincible = false;
        hasTriggered = false;
    }
}
