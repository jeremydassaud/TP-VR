using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class QuizManager : MonoBehaviour
{
    [Header("UI References - Glissez les GameObjects contenant les composants Text")]
    public GameObject questionObject;
    public GameObject answer1Object;
    public GameObject answer2Object;
    public Button answer1Button;
    public Button answer2Button;

    [Header("Teleportation")]
    public Transform playerTransform;
    public Transform nextQuizRoomPosition;
    public Transform thisQuizWrongAnswerRoomPosition;
    public float teleportDelay = 2f;

    [Header("Scene Management")]
    public bool isLastQuiz = false;
    public string nextSceneName = "";
    public float sceneChangeDelay = 3f;

    [Header("Success Animation")]
    public SuccessAnimationManager successAnimationManager;

    private Text questionText;
    private Text answer1Text;
    private Text answer2Text;

    private TextMeshProUGUI questionTextTMP;
    private TextMeshProUGUI answer1TextTMP;
    private TextMeshProUGUI answer2TextTMP;

    [Header("Quiz Data")]
    public string question = "Quelle est la capitale de la France ?";
    public string answer1 = "Paris";
    public string answer2 = "Londres";
    public bool answer1IsCorrect = true;
    public bool answer2IsCorrect = false;

    void Start()
    {
        GetTextComponents();
        SetupQuiz();
        SetupButtons();
    }

    void GetTextComponents()
    {
        if (questionObject != null)
        {
            questionText = questionObject.GetComponent<Text>();
            if (questionText == null)
                questionTextTMP = questionObject.GetComponent<TextMeshProUGUI>();
        }

        if (answer1Object != null)
        {
            answer1Text = answer1Object.GetComponent<Text>();
            if (answer1Text == null)
                answer1TextTMP = answer1Object.GetComponent<TextMeshProUGUI>();
        }

        if (answer2Object != null)
        {
            answer2Text = answer2Object.GetComponent<Text>();
            if (answer2Text == null)
                answer2TextTMP = answer2Object.GetComponent<TextMeshProUGUI>();
        }
    }

    void SetupQuiz()
    {
        if (questionText != null)
            questionText.text = question;
        else if (questionTextTMP != null)
            questionTextTMP.text = question;

        if (answer1Text != null)
            answer1Text.text = answer1;
        else if (answer1TextTMP != null)
            answer1TextTMP.text = answer1;

        if (answer2Text != null)
            answer2Text.text = answer2;
        else if (answer2TextTMP != null)
            answer2TextTMP.text = answer2;
    }

    void SetupButtons()
    {
        if (answer1Button != null)
            answer1Button.onClick.AddListener(() => OnAnswerSelected(1));
        if (answer2Button != null)
            answer2Button.onClick.AddListener(() => OnAnswerSelected(2));
    }

    public void OnAnswerSelected(int answerNumber)
    {
        bool isCorrect = false;
        string selectedAnswer = "";

        if (answerNumber == 1)
        {
            isCorrect = answer1IsCorrect;
            selectedAnswer = answer1;
        }
        else if (answerNumber == 2)
        {
            isCorrect = answer2IsCorrect;
            selectedAnswer = answer2;
        }

        Debug.Log($"=== QUIZ DEBUG ===");
        Debug.Log($"Question: {question}");
        Debug.Log($"Réponse sélectionnée: {selectedAnswer} (numéro {answerNumber})");
        Debug.Log($"Answer1IsCorrect = {answer1IsCorrect}");
        Debug.Log($"Answer2IsCorrect = {answer2IsCorrect}");
        Debug.Log($"Résultat: {(isCorrect ? "VRAI" : "FAUX")}");
        Debug.Log($"==================");

        Debug.Log(isCorrect ? "VRAI - Bonne réponse !" : "FAUX - Mauvaise réponse !");

        DisableButtons();

        if (isCorrect)
        {
            if (successAnimationManager != null)
            {
                successAnimationManager.PlaySuccessAnimation();
            }

            StartCoroutine(TeleportToNextRoom());
        }
        else
        {
            StartCoroutine(TeleportToWrongAnswerRoom());
        }
    }

    System.Collections.IEnumerator TeleportToNextRoom()
    {
        Debug.Log($"Téléportation vers la prochaine salle dans {teleportDelay} secondes...");
        yield return new WaitForSeconds(teleportDelay);

        if (isLastQuiz && !string.IsNullOrEmpty(nextSceneName))
        {
            Debug.Log($"Dernier quiz terminé ! Changement de scène vers '{nextSceneName}' dans {sceneChangeDelay} secondes...");
            yield return new WaitForSeconds(sceneChangeDelay);
            ChangeScene();
        }
        else if (playerTransform != null && nextQuizRoomPosition != null)
        {
            playerTransform.position = nextQuizRoomPosition.position;
            playerTransform.rotation = nextQuizRoomPosition.rotation;
            Debug.Log("Joueur téléporté vers la prochaine salle de quiz !");
        }
        else
        {
            Debug.LogWarning("PlayerTransform ou NextQuizRoomPosition non assigné !");
        }
    }

    void ChangeScene()
    {
        Debug.Log($"Changement de scène vers: {nextSceneName}");
        SceneManager.LoadScene(nextSceneName);
    }

    System.Collections.IEnumerator TeleportToWrongAnswerRoom()
    {
        Debug.Log($"Téléportation vers la salle d'erreur de ce quiz dans {teleportDelay} secondes...");
        yield return new WaitForSeconds(teleportDelay);

        if (playerTransform != null && thisQuizWrongAnswerRoomPosition != null)
        {
            playerTransform.position = thisQuizWrongAnswerRoomPosition.position;
            playerTransform.rotation = thisQuizWrongAnswerRoomPosition.rotation;
            Debug.Log("Joueur téléporté vers la salle d'erreur de ce quiz !");
        }
        else
        {
            Debug.LogWarning("PlayerTransform ou ThisQuizWrongAnswerRoomPosition non assigné !");
        }
    }

    void DisableButtons()
    {
        if (answer1Button != null)
            answer1Button.interactable = false;
        if (answer2Button != null)
            answer2Button.interactable = false;
    }

    public void EnableButtons()
    {
        if (answer1Button != null)
            answer1Button.interactable = true;
        if (answer2Button != null)
            answer2Button.interactable = true;
    }
}