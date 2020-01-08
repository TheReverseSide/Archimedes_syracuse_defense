using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsScreen : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        if (farthestLevelText != null)
        {
            farthestLevelText.text = "" + PlayerStats.farthestStage;
        }
        else if (currentPointsText != null)
        {
            currentPointsText.text = "" + PlayerStats.currentsPoints;
        }
        //else if (currentBrainHealthText != null)
        //{
        //    currentBrainHealthText.text = "" + PlayerStats.currentBrainHealth;
        //}
        else if (currentGlucoseText != null)
        {
            currentGlucoseText.text = "" + PlayerStats.currentGlucose;
        }
        else if (totalKillsText != null)
        {
            totalKillsText.text = "" + PlayerStats.enemiesKilled;
        }
        else if (totalCorrectQuestionsText != null)
        {
            totalCorrectQuestionsText.text = "" + PlayerStats.totalQuestionsCorrect;
        }
        else if (longestStreakText != null)
        {
            longestStreakText.text = "" + PlayerStats.longestStreak;
        }
    }

    [Header("Text")]
    [Header("Current")]
    public Text currentPointsText;
    public Text currentBrainHealthText;
    public Text currentGlucoseText;

    [Header("Total")]
    public Text farthestLevelText;
    public Text totalPointsText;

    public Text totalKillsText;
    public Text totalCorrectQuestionsText;
    public Text longestStreakText;

}
