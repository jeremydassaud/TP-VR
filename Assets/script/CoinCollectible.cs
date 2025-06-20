using UnityEngine;

public class CoinCollectible : MonoBehaviour
{
    [Header("Configuration")]
    public string coinID = "coin1"; // Identifiant unique

    public void OnObjectGrabbed()
    {
        Debug.Log("Objet collecté via UnityEvent !");

        PlayerPrefs.SetInt(coinID, 1); // Marque comme récupéré
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
        Debug.Log("Variable passée à true !");
    }

    public void PlaySound()
    {
        AudioSource audio = GetComponent<AudioSource>();
        if(audio != null)
            audio.Play();
    }
}