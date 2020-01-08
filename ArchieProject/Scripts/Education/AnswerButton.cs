using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerButton : MonoBehaviour
{
    //public Text answerText;
    public Text answerText;

    GameController gameController;
    AnswerData answerData;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    public void HandleClick()
    {
        gameController.AnswerButtonClicked(answerData.isCorrect, answerData.incorrectExplanation);

    }

    public void SetupU(AnswerData data)
    {
        answerData = data;
        //answerText.text = answerData.answerText;
        answerText.text = answerData.answerText;
    }
}
