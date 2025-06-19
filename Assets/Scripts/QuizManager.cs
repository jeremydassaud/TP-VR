using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    [Header("UI References - Glissez les GameObjects contenant les composants Text")]
    public GameObject questionObject;
    public GameObject answer1Object;
    public GameObject answer2Object;

    private Text questionText;
    private Text answer1Text;
    private Text answer2Text;

    private TextMeshProUGUI questionTextTMP;
    private TextMeshProUGUI answer1TextTMP;
    private TextMeshProUGUI answer2TextTMP;
    public Button answer1Button;
    public Button answer2Button;

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
        answer1Button.onClick.AddListener(() => OnAnswerSelected(1));
        answer2Button.onClick.AddListener(() => OnAnswerSelected(2));
    }

    public void OnAnswerSelected(int answerNumber)
    {
        bool isCorrect = false;

        if (answerNumber == 1)
        {
            isCorrect = answer1IsCorrect;
        }
        else if (answerNumber == 2)
        {
            isCorrect = !answer1IsCorrect;
        }

        Debug.Log(isCorrect ? "VRAI - Bonne réponse !" : "FAUX - Mauvaise réponse !");
    }

    void DisableButtons()
    {
        answer1Button.interactable = false;
        answer2Button.interactable = false;
    }

    public void EnableButtons()
    {
        answer1Button.interactable = true;
        answer2Button.interactable = true;
    }
}