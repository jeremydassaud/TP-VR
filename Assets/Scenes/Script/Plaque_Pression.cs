using UnityEngine;

public class Plaque_Pression : MonoBehaviour
{
    public float descentAmount = 0.1f; // Mont�e de la plaque
    public float descentSpeed = 2f; // Vitesse de descente
    private Vector3 originalPosition; // Position originale de la plaque
    private bool isActivated = false; // �tat de la plaque
    private PressurePlateManager manager; // R�f�rence au gestionnaire

    void Start()
    {
        originalPosition = transform.position; // Stocke la position originale
        manager = FindFirstObjectByType<PressurePlateManager>(); // Trouve le gestionnaire
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish") && !isActivated)
        {
            ActivatePlate();
        }
    }

    void ActivatePlate()
    {
        isActivated = true; // Marque la plaque comme activ�e
        manager.ActivatePlate(); // Informe le gestionnaire qu'une plaque a �t� activ�e
        StartCoroutine(DescendrePlate());
    }

    System.Collections.IEnumerator DescendrePlate()
    {
        Vector3 targetPosition = originalPosition - new Vector3(0, descentAmount, 0);
        while (transform.position.y > targetPosition.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, descentSpeed * Time.deltaTime);
            yield return null;
        }
    }
}