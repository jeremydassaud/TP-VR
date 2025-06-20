using UnityEngine;
public class SkullCollected : MonoBehaviour
{
    public string itemID = "ossement";

    public void OnObjectGrabbed()
    {
        Debug.Log("Objet collecté via UnityEvent !");
        PlayerPrefs.SetInt(itemID, 1);
        PlayerPrefs.Save();
        gameObject.SetActive(false);
    }

    public void DestroyObject() => Destroy(gameObject);

    public void SetVariableTrue()
    {
        PlayerPrefs.SetInt(itemID, 1);
        PlayerPrefs.Save();
        Debug.Log("Variable passée à true !");
    }

    public void PlaySound()
    {
        AudioSource audio = GetComponent<AudioSource>();
        if(audio != null) audio.Play();
    }
}