using UnityEngine;

public class CoinCollectible : MonoBehaviour
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
        if(audio != null)
            audio.Play();
    }
}