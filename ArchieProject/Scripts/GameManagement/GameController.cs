using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public ObjectPool answerButtonObjectPool;
    public Transform answerButtonParent;

    #region Quiz and Game completion
    public static bool occipitalQuizComplete;
    public static bool frontalQuizComplete;
    public static bool parietalQuizComplete;
    public static bool temporalQuizComplete;
    public static bool cerebellumQuizComplete;
    public static bool brainStemQuizComplete;

    public static bool occipitalGameComplete;
    public static bool frontalGameComplete;
    public static bool parietalGameComplete;
    public static bool temporalGameComplete;
    public static bool cerebellumGameComplete;
    public static bool brainStemGameComplete;
    #endregion

    #region Scenes visited 
    //Check whether they have visited this before to prevent popups from popping again
    public static bool hasVisitedLogin;
    public static bool hasVisitedLobeSelector;
    public static bool hasVisitedLS;
    public static bool hasVisitedFirstGP;
    public static bool hasReviewed;
    #endregion


    [Header("Canvases")]
    public Canvas infoScorePanel;
    public Canvas questionPanel;
    public Canvas answerRect;
    public Canvas roundOverCanvas; //is this the same as score_rewards?
    public Canvas Score_Rewards_PlayThis;

    [Header("Button Canvases")]
    public Canvas nextButtonCanvas;
    public Canvas reviewButtonCanvas;
    public Canvas playButtonCanvas;

    [Header("Lobe intro Canvases")]
    public Canvas firstEverCanvasAndFirstLobe; //Introduce the lobe as well as the concept of the quizzes
    //public Canvas occipitalLobeIntro;
    //public Canvas temporalLobeIntro;
    //public Canvas frontalLobeIntro;
    //public Canvas parietalLobeIntro;
    //public Canvas occipitalLobeIntro;

    [Header("QuestionRelated")]
    public Text questionText;
    public Text questionNumberText;



    [Header("Stats")] /*
    public Text currencyGained;
    public Text correctQuestions;
    public Text percentCorrect;
    public Text performanceBonuses;
     public Text longestStreakThisRoundText;
     */
    public Text answerResponse;
    public Text pointsText;
    public Text glucoseText;
    int glucoseTracker = 0;
    public Text pointsText2;
    public Text streakDisplayText;
    public Text bottomQuestionNumber;

    [Header("Buttons")]
    public Button nextButton;
    public Button reviewScreenButton;
    public Button toGameplayButton;

    public GameObject brainImageObject;

    QuestionDataController dataController;
    QuestionRoundData currentRoundData;
    QuestionData[] questionPool;
    AnswerData answerData;
    //public GameObject answerResponseGO;

    int currentStreak;
    int longestStreakThisRound;

    int totalQuestions;
    //bool isRoundActive;
    int questionIndex; //Location in question array
    List<GameObject> answerButtonGameObjects = new List<GameObject>();


    void Start()
    {
        dataController = FindObjectOfType<QuestionDataController>(); //Finds the only dataController because it is static

        currentRoundData = dataController.GetQuestionRoundData();
        questionPool = currentRoundData.questions;

        totalQuestions = questionPool.Length; //Only gets questions per round

        ShowQuestion();
        //isRoundActive = true; //CUIDAO - this may need to be turned off when entering gameplay, then turned back on again

        nextButton.enabled = false;
        nextButton.image.GetComponent<Image>().color = new Color32(61, 60, 55, 100);
    }

    private void CheckQuestionStatus(QuestionData questionData)
    {
        //Get question number from question object
        if (!questionData.getIsLast())
        {//If there are still questions left
            reviewScreenButton.enabled = false;
            toGameplayButton.enabled = false;
        }
        else if (questionData.getIsLast())
        {//if this is the last, send to review screen

            nextButton.enabled = false;
            nextButtonCanvas.GetComponent<CanvasGroup>().alpha = 0;
            nextButtonCanvas.GetComponent<CanvasGroup>().interactable = false;
            nextButtonCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
            brainImageObject.SetActive(false);

            reviewScreenButton.enabled = true;
            reviewButtonCanvas.GetComponent<CanvasGroup>().alpha = 1;
            reviewButtonCanvas.GetComponent<CanvasGroup>().interactable = true;
            reviewButtonCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;

            questionData.CalcuatePerRoundStats();
        }
    }

    private void ShowQuestion()
    {

        if (PlayerStats.currentLobeIndex == 0 && questionIndex == 0) //O_ Will this work now that quizzes are optional? 
        { //Here check to see if it is the first question of the first round = introduce overall concept. 
            if (firstEverCanvasAndFirstLobe != null)
            {
                firstEverCanvasAndFirstLobe.GetComponent<CanvasGroup>().alpha = 0;
                firstEverCanvasAndFirstLobe.GetComponent<CanvasGroup>().interactable = false;
                firstEverCanvasAndFirstLobe.GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        }
        /*else if (questionIndex == 0)
        { //it is the first of that particular lobe, introduce the lobe
            if(){ //Check the lobe name and give appropriate introduction - how can I get the name?

            }
        }*/

        answerResponse.text = "";

        RemoveAnswerButtons();

        QuestionData questionData = questionPool[questionIndex];

        CheckQuestionStatus(questionData);

        questionText.text = questionData.questionText;

        int questionCounter = questionIndex;
        questionNumberText.text = " Question " + (questionCounter + 1) + ": ";

        for (int i = 0; i < questionData.answers.Length; i++)
        {
            GameObject answerButtonGameObject = answerButtonObjectPool.GetObject(); //gets next unused button in the pool
            answerButtonGameObject.transform.SetParent(answerButtonParent, false);
            answerButtonGameObjects.Add(answerButtonGameObject);
            //answerButtonGameObject.transform.localScale = Vector3.one;

            AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton>();

            answerButton.SetupU(questionData.answers[i]);
        }

        bottomQuestionNumber.text = "Question " + (questionCounter + 1) + " of 4";

        questionCounter++;

    }

    private void RemoveAnswerButtons()
    { //Removes all answer buttons before instantiating new ones
        while (answerButtonGameObjects.Count > 0)
        {
            answerButtonObjectPool.ReturnObject(answerButtonGameObjects[0]); //removes from active pool
            answerButtonGameObjects.RemoveAt(0); //removes from list
        }
    }

    public void AnswerButtonClicked(bool isCorrect, string incorrectExplanation)
    {
        answerRect.GetComponent<CanvasGroup>().interactable = false;
        answerRect.GetComponent<CanvasGroup>().alpha = .5f;

        if (isCorrect)
        {
            //AudioManager.instance.Play(""); Audio needed

            PlayerStats.totalQuestionsCorrect++;
            PlayerStats.currentsPoints += currentRoundData.questionValue;
            pointsText.text = "Score: " + PlayerStats.currentsPoints;

            glucoseTracker += 20;
            glucoseText.text = "Glucose: " + glucoseTracker;

            answerResponse.text = "Correct!";
            answerResponse.color = Color.green;
            answerResponse.GetComponent<Animation>().Play("answerResponseReveal");

            //PlayerStats.longestStreak++;
            handleStreak();
        }
        else
        {
            AudioManager.instance.Play("IncorrectAnswer");

            answerResponse.text = "Incorrect... " + incorrectExplanation;
            answerResponse.color = Color.red;
            answerResponse.GetComponent<Animation>().Play("answerResponseReveal");

            //PlayerStats.longestStreak = 0;
            currentStreak = 0;
        }
        nextButton.enabled = true;
        nextButton.image.GetComponent<Image>().color = new Color32(221, 221, 221, 100);
    }

    void handleStreak()
    {
        currentStreak++;

        if (currentStreak > PlayerStats.longestStreak)
        {
            PlayerStats.longestStreak = currentStreak;
        }

        if (currentStreak > longestStreakThisRound)
        {
            longestStreakThisRound = currentStreak;
        }

        if (currentStreak >= 3)
        {
            //Show text displaying streak
            streakDisplayText.text = "Current streak: " + currentStreak + "!!";
        }
        else
        {
            streakDisplayText.text = "";
        }
    }

    public void NextButton()
    { //Should move to next question set, or if ended, move to end of round screen
        answerRect.GetComponent<CanvasGroup>().interactable = true;
        answerRect.GetComponent<CanvasGroup>().alpha = 1;

        nextButton.enabled = false;
        nextButton.image.GetComponent<Image>().color = new Color32(61, 60, 55, 100);

        if (questionPool.Length > questionIndex + 1)
        {
            questionIndex++;
            ShowQuestion();
        }
        else
        {
            PlayerStats.farthestStage++; //Progressed to next level, adjust, which will in turn adjust which brain image is used on the lobe selector screen

            //Do math calculations or call method here and set all text
            //(percentCorrect.text = "Percent of questions correct: " + Mathf.RoundToInt(((PlayerStats.questionsCorrectThisRound + .0000001f) / questionPool.Length) * 100) + "%";
            //float percentCorrectFloat = Mathf.RoundToInt(((PlayerStats.questionsCorrectThisRound + .0000001f) / questionPool.Length) * 100);

            questionPanel.enabled = false;
            questionPanel.GetComponent<CanvasGroup>().alpha = 0;
            questionPanel.GetComponent<CanvasGroup>().interactable = false;

            brainImageObject.SetActive(false);

            bottomQuestionNumber.text = "";

            roundOverCanvas.enabled = true;
            roundOverCanvas.GetComponent<CanvasGroup>().alpha = 1;
            roundOverCanvas.GetComponent<CanvasGroup>().interactable = true;

            Score_Rewards_PlayThis.enabled = true;
            Score_Rewards_PlayThis.GetComponent<CanvasGroup>().alpha = 1;
            Score_Rewards_PlayThis.GetComponent<CanvasGroup>().interactable = true;
            /* REMOVAL FOR PURPOSES OF VIDEO
            longestStreakThisRoundText.text = longestStreakThisRound.ToString();

            string sceneName = SceneManager.GetActiveScene().ToString();

            foreach (var quiz in PlayerStats.quizList)
            {
                if (quiz.name.Contains(sceneName))
                {
                    quiz.beaten = true;
                    quiz.score = percentCorrectFloat;
                }
            }
            */
            EndRound();
        }
    }

    public void EndRound()
    {
        //pointsText2.text = "Score: " + PlayerStats.currentsPoints; //This is total points, not gathered per round
        //correctQuestions.text = "Total questions scored correctly: " + PlayerStats.totalQuestionsCorrect;
        //performanceBonuses.text = "Need to implement bonuses";

        PlayerStats.currentLobeIndex++;
        //isRoundActive = false;

        if (currentStreak > 3)
        {
            //Connect to streak text + bonus (extra cards)
        }

        questionPanel.GetComponent<CanvasGroup>().alpha = 0;
        questionPanel.GetComponent<CanvasGroup>().interactable = false;
        questionPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;

        infoScorePanel.GetComponent<CanvasGroup>().alpha = 0;
        infoScorePanel.GetComponent<CanvasGroup>().interactable = false;
        infoScorePanel.GetComponent<CanvasGroup>().blocksRaycasts = false;

        roundOverCanvas.GetComponent<CanvasGroup>().alpha = 1;
        roundOverCanvas.GetComponent<CanvasGroup>().interactable = true;
        roundOverCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;


    }

    public int NumberOfQuestions()
    {
        return totalQuestions;
    }

    public void GoToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
