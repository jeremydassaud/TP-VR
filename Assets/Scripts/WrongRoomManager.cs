using UnityEngine;
using UnityEngine.SceneManagement;

public class WrongRoomManager : MonoBehaviour
{
    [Header("Room Settings")]
    public Transform playerTransform;
    public Transform nextQuizRoomDestination;
    public PressurePlate[] pressurePlates;

    [Header("Scene Management")]
    public bool isLastWrongRoom = false;
    public string nextSceneName = "";

    [Header("Room Configuration")]
    [Range(3, 10)]
    public int numberOfPlates = 5;
    public int correctPlateIndex = 0;

    void Start()
    {
        SetupPressurePlates();
    }

    void SetupPressurePlates()
    {
        for (int i = 0; i < pressurePlates.Length; i++)
        {
            if (pressurePlates[i] != null)
            {
                pressurePlates[i].playerTransform = playerTransform;
                pressurePlates[i].teleportDestination = nextQuizRoomDestination;

                pressurePlates[i].isCorrectPlate = (i == correctPlateIndex);

                if (isLastWrongRoom)
                {
                    pressurePlates[i].isLastPressurePlate = pressurePlates[i].isCorrectPlate;
                    pressurePlates[i].nextSceneName = nextSceneName;
                }

                Debug.Log($"Plaque {i + 1}: {(pressurePlates[i].isCorrectPlate ? "CORRECTE" : "incorrecte")}");
                if (isLastWrongRoom && pressurePlates[i].isCorrectPlate)
                {
                    Debug.Log($"Cette plaque changera de scène vers: {nextSceneName}");
                }
            }
        }
    }

    [ContextMenu("Randomize Correct Plate")]
    public void RandomizeCorrectPlate()
    {
        if (pressurePlates.Length > 0)
        {
            correctPlateIndex = Random.Range(0, pressurePlates.Length);
            SetupPressurePlates();
            Debug.Log($"Nouvelle plaque correcte: {correctPlateIndex + 1}");
        }
    }
}