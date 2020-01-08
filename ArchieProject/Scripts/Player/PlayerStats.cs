using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class PlayerStats : SerializedMonoBehaviour
{
    #region Variables and properties
    [Header("Variables")]
    public static float currentGlucose;
    public float startGlucose = 1060;

    public static float Health = 0;
    public float startHealth = 0;

    public static string Name;
    public static bool isGuest;

    public static int longestStreak;

    public static int currentsPoints;

    public int startPoints = 300;


    //LEVEL MANAGEMENT
    //Lobes (levels) > Stages (I want this be called levels) > waves (rounds)

    public static int currentLobeIndex; //These are overall levels (Occipital game scene) - The
    public int startCurrentLobeIndex = 0;

    //Stages

    public static int rounds; //These are waves (?)

    [ValueDropdown("CardDeck", ExpandAllMenuItems = true)]
    public static List<GameObject> cardDeck;

    public static List<Stages> stageList;

    public static List<Quiz> quizList;

    [Header("Cards")]
    public GameObject glucose_50_CIED;
    public GameObject glucose_100_CIED;
    public GameObject glucose_250_CIED;

    public GameObject slowGlialCIED;
    public GameObject csfGlialCIED;
    public GameObject electricGlialCIED;

    public GameObject CannonTurret_Base_CIED;
    //Other cannons
    public GameObject GunTurret_Base_CIED;
    //Other guns
    public GameObject LaserTurret_Base_CIED;
    //Other lasers

    [HideInInspector]
    public static int farthestStage;
    int startFarthestStage = 0;

    public static string currentStageName;

    [Header("This round temps")]
    public static float questionsCorrectThisRound;
    public static int pointsThisRound;
    public static int glucoseGainedThisRound;

    [Header("Totals")]
    [HideInInspector]
    public static int totalGlucoseLevel;
    [HideInInspector]
    public static int totalQuestionsCorrect;
    [HideInInspector]
    public static int enemiesKilled;

    public Image StageOneOneGridImage;
    public Image StageOneTwoGridImage;
    public Image StageOneThreeGridImage;
    public Image StageOneFourGridImage;


    [ShowInInspector]
    public static IDictionary<int, string> levelDIctionary = new Dictionary<int, string>() {
        {0, "Stage1.1"},
        {1, "Stage1.2"},
        {2, "Stage1.3"},
        {3, "Stage1.4"},
        {4, "Stage2.1"},
        {5, "Stage2.2"},
        {6, "Stage2.3"},
        {7, "Stage2.4"},
        {8, "Stage3.1"},
        {9, "Stage3.2"},
        {10, "Stage3.3"},
        {11, "Stage3.4"},
        {12, "Stage4.1"},
        {13, "Stage4.2"},
        {14, "Stage4.3"},
        {15, "Stage4.4"}
    };
    #endregion Variables and properties

    void Start()
    {
        //Debug.Log("Starting playerStats");

        currentGlucose = 1000;
        currentsPoints = 0;
        Health = startHealth;
        totalQuestionsCorrect = 0;
        farthestStage = startFarthestStage;
        currentLobeIndex = 0;

        rounds = 0; //These are actually waves
        enemiesKilled = 0;
        totalGlucoseLevel = 0;
        longestStreak = 0;

        if (cardDeck == null)
        {
            cardDeck = new List<GameObject>();

            //Debug.Log("creating deck");

            //PlayerStats.cardDeck.Add(glucose_50_CIED);
            //PlayerStats.cardDeck.Add(slowGlialCIED);
            PlayerStats.cardDeck.Add(CannonTurret_Base_CIED);
            PlayerStats.cardDeck.Add(GunTurret_Base_CIED);
        }

        if (stageList == null)
        {
            stageList = new List<Stages>();

            //STAGE ONE
            Stages stageOneOne = new Stages("Stage1.1", 1, 0, false, "Stage1.2");
            Stages stageOneTwo = new Stages("Stage1.2", 2, 0, false, "Stage1.3");
            Stages stageOneThree = new Stages("Stage1.3", 3, 0, false, "Stage1.4");
            Stages stageOneFour = new Stages("Stage1.4", 4, 0, false, "Stage2.1");
        }

        /* REMOVAL FOR PURPOSES OF VIDEO
        Quiz quizOne = new Quiz("QuizOne", 0, 0, false, "QuizTwo");
        quizList.Add(quizOne);
        Quiz quizTwo = new Quiz("QuizTwo", 1, 0, false, "QuizThree");
        quizList.Add(quizTwo);
        Quiz quizThree = new Quiz("QuizThree", 2, 0, false, "QuizFour");
        quizList.Add(quizThree);
        Quiz quizFour = new Quiz("QuizFour", 3, 0, false, "QuizFive");
        quizList.Add(quizFour);
        Quiz quizFive = new Quiz("QuizFive", 4, 0, false, "QuizSix");
        quizList.Add(quizFive);
        Quiz quizSix = new Quiz("QuizSix", 5, 0, false, "");
        quizList.Add(quizSix);
        Debug.Log("quiz now populated," + quizList);
        */
    }



    public void SetGuest()
    {
        PlayerStats.isGuest = true;
    }

    public void PopulateHand(GameObject DropZone)
    {
        //Debug.Log("made it to method");
        if (cardDeck.Count > 1)
        {
            foreach (var card in cardDeck)
            {
                Instantiate(card, DropZone.transform);
            }
        }
        else { return; }
    }

    public void FindGridImage(string stageName)
    {
        if (stageName.Equals("Stage1.1"))
        {
            ChangeGridColorToComplete(StageOneOneGridImage);
        }
        else if (stageName.Equals("Stage1.2"))
        {
            ChangeGridColorToComplete(StageOneTwoGridImage);
        }
        else if (stageName.Equals("Stage1.3"))
        {
            ChangeGridColorToComplete(StageOneThreeGridImage);
        }
        else if (stageName.Equals("Stage1.4"))
        {
            ChangeGridColorToComplete(StageOneFourGridImage);
        }
    }

    public void ChangeGridColorToComplete(Image image)
    {
        image.GetComponent<Image>().color = new Color32(61, 60, 55, 100); //O_ find new color
    }
}





