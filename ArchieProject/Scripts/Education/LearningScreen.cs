using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class LearningScreen : MonoBehaviour
{
    [Header("During quiz game stats")]
    public Text glucoseLevel;
    public Text currentPoints;

    [Header("Score_rewards_play screen stats")]
    public Text pointsThisRound;
    public Text questionsCorrectThisRound;
    public Text percentageCorrect;
    public Text glucoseGainedThisRound;
    public Text performanceBonuses;

    [Header("Canvases")]
    public Canvas scoreScreenAfterQuiz;
    public Canvas questionairreScreen;
    public Canvas reviewScreenCanvas;
    public Canvas toGameplayCanvas;


    [Header("Buttons")]
    public Button toGameplayButton;
    public Button reviewScreenButton;

    private void FixedUpdate()
    {
        //DURING QUIZ
        //glucoseLevel.text = " Glucose: " + PlayerStats.totalGlucoseLevel;
        currentPoints.text = " Score: " + PlayerStats.currentsPoints;

        //AFTER QUIZ REVIEW
        //pointsThisRound.text = "Points earned this round: " + PlayerStats.pointsThisRound;
        //glucoseGainedThisRound.text = "Glucose earned this round: " + PlayerStats.glucoseGainedThisRound;
        //questionsCorrectThisRound.text = "Questions correctly answered: " + PlayerStats.questionsCorrectThisRound;
        //performanceBonuses.text = "Performance bonuses earned this round: ";
    }

    public void skipToGameplay()
    {
        SceneManager.LoadScene(PlayerStats.currentLobeIndex + 1); //SHOULD add one to the current level (odds are questions, evens are gameplays)
    }

    public void revealScoreAfterQuiz()
    {
        GameController.hasReviewed = true;

        reviewScreenButton.enabled = false;
        reviewScreenCanvas.GetComponent<CanvasGroup>().alpha = 0;
        reviewScreenCanvas.GetComponent<CanvasGroup>().interactable = false;
        reviewScreenCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;

        scoreScreenAfterQuiz.GetComponent<CanvasGroup>().alpha = 1;
        scoreScreenAfterQuiz.GetComponent<CanvasGroup>().interactable = true;
        scoreScreenAfterQuiz.GetComponent<CanvasGroup>().blocksRaycasts = true;

        questionairreScreen.GetComponent<CanvasGroup>().alpha = 0;
        questionairreScreen.GetComponent<CanvasGroup>().interactable = false;
        questionairreScreen.GetComponent<CanvasGroup>().blocksRaycasts = false;

        toGameplayButton.enabled = true;
        toGameplayCanvas.GetComponent<CanvasGroup>().alpha = 1;
        toGameplayCanvas.GetComponent<CanvasGroup>().interactable = true;
        toGameplayCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
