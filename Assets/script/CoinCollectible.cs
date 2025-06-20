using UnityEngine;

public class CoinCollectible : MonoBehaviour
{
    [Header("Configuration")]
    public string coinID = "coin1"; // Identifiant unique

    public void OnObjectGrabbed()
    {
        Debug.Log("Objet collect� via UnityEvent !");

        PlayerPrefs.SetInt(coinID, 1); // Marque comme r�cup�r�
        PlayerPrefs.Save();

        gameObject.SetActive(false); // Cache l'objet
     }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    public void SetVariableTrue()
    {
        PlayerPrefs.SetInt(coinID, 1);
        PlayerPrefs.Save();
        Debug.Log("Variable pass�e � true !");
    }

    public void PlaySound()
    {
        AudioSource audio = GetComponent<AudioSource>();
        if(audio != null)
            audio.Play();
    }
}