using UnityEngine;

public class CoinCollectible : MonoBehaviour
{
    [Header("Configuration")]
    public bool isCollected = false;

    // Fonction publique que tu peux appeler depuis l'�v�nement Unity
    public void OnObjectGrabbed()
    {
        Debug.Log("Objet collect� via UnityEvent !");

        // Passe la variable � true
        isCollected = true;

        // Faire dispara�tre l'objet
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
        Debug.Log("Variable pass�e � true !");
    }

    public void PlaySound()
    {
        AudioSource audio = GetComponent<AudioSource>();
        if(audio != null)
            audio.Play();
    }
}