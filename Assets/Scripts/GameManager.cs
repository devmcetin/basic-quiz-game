using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Question[] questions;
    private static List<Question> unansweredQuestions;

    private Question currentQuestion;

    [SerializeField] private Text factText;

    [SerializeField] private Text correctAnswerText;
    [SerializeField] private Text wrongAnswerText;

    [SerializeField] private Animator animator;

    [SerializeField] private float interval = 1f;

    private void Start()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<Question>();
        }

        SetCurrentQuestion();
    }

    private void SetCurrentQuestion()
    {
        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];

        factText.text = currentQuestion.fact;

        if (currentQuestion.isTrue)
        {
            correctAnswerText.text = "CORRECT";
            wrongAnswerText.text = "WRONG";
        }
        else
        {
            correctAnswerText.text = "WRONG";
            wrongAnswerText.text = "CORRECT";
        }
    }

    private IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(interval);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UserSelectTrue()
    {
        animator.SetTrigger("True");

        if (currentQuestion.isTrue)
        {
            Debug.Log("Correct !");
        }
        else
        {
            Debug.Log("Wrong !");
        }

        StartCoroutine(TransitionToNextQuestion());
    }

    public void UserSelectFalse()
    {
        animator.SetTrigger("False");

        if (!currentQuestion.isTrue)
        {
            Debug.Log("Correct !");
        }
        else
        {
            Debug.Log("Wrong !");
        }

        StartCoroutine(TransitionToNextQuestion());
    }
}