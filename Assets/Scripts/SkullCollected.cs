using UnityEngine;

public class SkullCollected : MonoBehaviour
{
    [Header("Configuration")]
    public bool isCollected = false;

    // Fonction publique que tu peux appeler depuis l'événement Unity
    public void OnObjectGrabbed()
    {
        Debug.Log("Objet collecté via UnityEvent !");

        // Passe la variable à true
        isCollected = true;

        // Faire disparaître l'objet
        gameObject.SetActive(false);

        FindFirstObjectByType<Gestion_Grille_Ossement>().UpdateGrille(isCollected);
    }

    // Autres fonctions utiles que tu peux utiliser
    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    public void SetVariableTrue()
    {
        isCollected = true;
        Debug.Log("Variable passée à true !");
    }

    public void PlaySound()
    {
        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null)
            audio.Play();
    }
}