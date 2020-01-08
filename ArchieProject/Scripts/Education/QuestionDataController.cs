using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestionDataController : MonoBehaviour
{ //Starts our persistent DataController, creates array of rounds (lobeQuestionData according to whatever lobe the questions need to relate to)

    public static QuestionDataController dataControllerInstance;

    public QuestionRoundData[] lobeQuestionData; //Each is an object containing all of the questions for that lobe

    private void Awake()
    {
        if (dataControllerInstance != null) //to catch whether or not there is already a build manager in the scene
        {
            Debug.Log("More than one dataControllerInstance");
            return;
        }
        dataControllerInstance = this; //Setting a reference to itself to be accessed from other classes
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        SceneManager.LoadScene("LoginMenu");
    }

    public QuestionRoundData GetQuestionRoundData()
    {
        int currentLobe = PlayerStats.currentLobeIndex;
        int farthestLevel = PlayerStats.farthestStage;
        currentLobe++; //Offsetting for array
        farthestLevel++; //Offsetting for array

        //Wouldnt it be easier just to load this initially as the first round of questions, then the rest depending on whatever button they happened to press beacuse it is always accessible?
        if (currentLobe % 2 == 0)
        { //even number, a gameplay screen, we then want to load the previous LS questions because they are coming back to do them again or finish them

            if (farthestLevel == currentLobe)
            { //They have beaten the gameplay, which means they need to go to the next quiz
                Debug.Log("They have beaten the gameplay, which means they need to go to the next quiz");
                return lobeQuestionData[PlayerStats.currentLobeIndex++];
            }
            else
            { //They havent beat that gameplay screen, which means they are repeating the quiz for more points
                Debug.Log("They havent beat that gameplay screen, which means they are repeating the quiz for more points");
                return lobeQuestionData[PlayerStats.currentLobeIndex--];
            }
        }
        else
        { //Odd number: the correct LS
            return lobeQuestionData[PlayerStats.currentLobeIndex];
        }
    }
}

