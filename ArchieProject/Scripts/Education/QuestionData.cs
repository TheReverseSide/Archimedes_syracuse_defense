using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestionData
{
    public string questionName;

    [Header("Question Data")]
    public string questionText;
    public bool isLastQuestion;

    [Header("Question photos")]
    public Sprite questionSpriteOne;
    public Sprite questionSpriteTwo;

    [Header("Answers")]
    public AnswerData[] answers;

    private int pointsBeforeRound;
    private int questionsCorrectBeforeRound;
    private int questionsIncorrectBeforeRound;
    private int glucoseBeforeRound;

    void Start()
    {
        //Is this really going to work because this is an individual question, not the start of the round?
        pointsBeforeRound = PlayerStats.currentsPoints;
        questionsCorrectBeforeRound = PlayerStats.totalQuestionsCorrect;
        glucoseBeforeRound = PlayerStats.totalGlucoseLevel;
    }

    public bool getIsLast()
    {
        return isLastQuestion;
    }

    public void CalcuatePerRoundStats()
    {
        PlayerStats.pointsThisRound = PlayerStats.currentsPoints - pointsBeforeRound;
        PlayerStats.questionsCorrectThisRound = PlayerStats.totalQuestionsCorrect - questionsCorrectBeforeRound;
        PlayerStats.glucoseGainedThisRound = PlayerStats.totalGlucoseLevel - glucoseBeforeRound;
    }
}