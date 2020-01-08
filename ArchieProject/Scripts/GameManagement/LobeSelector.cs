using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class LobeSelector : MonoBehaviour
{
    public SpriteRenderer brainImage;
    public PlayerStats playerStats;

    //Organization strategy: Have the lobe selection screen the only (currently) way to load levels/stages. When a stage is loaded, it sets the PlayerStats.currentStageName;
    //to that name, then if a level is beaten and the player presses continue (GameManager && CompleteLevel)


    [Header("Frontal")]
    public Button FrontalLobeButton; //What does this do?
    public Button FrontalLobeQuizButton;
    public Button FrontalLobeGameButton; //Outdated, serves no function

    public Button FrontalLobeStageOneOneButton;
    public Button FrontalLobeStageOneTwoButton;
    public Button FrontalLobeStageOneThreeButton;
    public Button FrontalLobeStageOneFourButton;


    [Header("Occip")]
    public Button OccipitalLobeButton;
    public Button OccipitalLobeQuizButton;
    public Button OccipitalLobeGameButton;
    public GameObject LockOccip;

    [Header("Temporal")]
    public Button TemporalLobeButton;
    public Button TemporalLobeQuizButton;
    public Button TemporalLobeGameButton;
    public GameObject LockTemp;

    [Header("Parietal")]
    public Button ParietalLobeButton;
    public Button ParietalLobeQuizButton;
    public Button ParietalLobeGameButton;
    public GameObject LockPariet;

    [Header("Cerebellum")]
    public Button CerebellumButton;
    public Button CerebellumQuizButton;
    public Button CerebellumGameButton;
    public GameObject LockCere;

    [Header("Brain stem")]
    public Button BrainStemButton;
    public Button BrainStemQuizButton;
    public Button BrainStemGameButton;
    public GameObject LockBrain;

    /* Level one: Frontal lobeLS
    * Level two: Frontal lobe
    * Level three: Occipital lobeLS
    * Level four: Occipital lobe
    * Level five: Temporal lobeLS
    * Level six: Temporal lobe
    * Level seven: Parietal lobeLS
    * Level eight: Parietal lobe
    * Level nine: CerebellumLS
    * Level ten: Cerebellum
    * Level eleven: BrainStem lobeLS - Test
    * Level twelve: BrainStem lobe - Boss fight
    */

    private void Start() //This runs each time you open the screen effectively refreshing it
    {
        //Wat is it called when you make cases that depend on an int?
        //THIS IS NOT PERFECT AND WILL OVERRIDE ITSELF !!!!!!!
        //YOU DO NOT HAVE TO BEAT THE QUIZ TO PROGRESS OT THE NEXT LEVEL BUT YOU HAVE TO BEAT THE GAMEPLAY??

        playerStats = FindObjectOfType<PlayerStats>();

        if (PlayerStats.farthestStage == 0)
        {
            //Unlock Lobe 1 stage 1
            EnableStageOneOne();
            DisableStageOneTwo();
            DisableStageOneThree();
            DisableStageOneFour();


            //O_ Make method to enable/lobes in their entirety



            //NEED TO UPDATE FOR STAGES
            DisableOccipGame();
            DisableTempGame();
            DisableParietalGame();
            DisableCerebellumGame();
            DisableBrainStemGame();

            //Needs to be updated for stages
            DisableOccipQuiz();
            DisableTempQuiz();
            DisableParietalQuiz();
            DisableCerebellumQuiz();
            DisableBrainStemQuiz();
        }
        if (GameController.frontalGameComplete) //They beat Frontal, unlock Occipital
        {
            //DisableFrontalGame();

            EnableOccipGame();
            EnableOccipQuiz();
            //Implement code for switching images
        }
        if (GameController.occipitalGameComplete) //They beat Occipital, unlock Temporal
        {
            DisableOccipGame();

            EnableTempGame();
            EnableTempQuiz();
        }
        if (GameController.temporalGameComplete) //They beat temproal, unlock Parietal
        {
            DisableTempGame();

            EnableParietalGame();
            EnableParietalQuiz();
        }
        if (GameController.parietalGameComplete) //They beat parietal, unlock Cerebellum
        {
            DisableFrontalQuiz();

            EnableCerebellumGame();
            EnableCerebellumQuiz();
        }
        if (GameController.cerebellumGameComplete) //They beat Cerebellum, unlock Brainstem
        {
            DisableCerebellumGame();

            EnableBrainStemGame();
            EnableBrainStemQuiz();
        }


        //It would be nice to differentiate between locked and beaten (more locks?)
        if (GameController.frontalQuizComplete)
        {
            DisableFrontalQuiz();
        }
        if (GameController.occipitalQuizComplete)
        {
            DisableOccipQuiz();
        }
        if (GameController.temporalQuizComplete)
        {
            DisableTempQuiz();
        }
        if (GameController.parietalQuizComplete)
        {
            DisableParietalQuiz();
        }
        if (GameController.cerebellumQuizComplete)
        {
            DisableCerebellumQuiz();
        }
        if (GameController.brainStemQuizComplete)
        {
            DisableBrainStemQuiz();
        }
    }


    //List in order:
    //Brain-Part-1-Unlocked 1
    //Brain-1-4
    //Brain-1-3-5
    //Brain-Part-1-2-3-4-Unlocked 1
    //Brain-Part-1-2-3-4-5-Unlocked 1
    //Brain-Completed 1
    public void ChangeImage(string newImageTitle) //Where should I call this from?
    {
        brainImage.sprite = Resources.Load<Sprite>("Sprites/" + newImageTitle);
    }


    #region FRONTAL LOBE
    //DISABLE FRONTAL 
    //void DisableFrontalGame()
    //{
    //    FrontalLobeGameButton.enabled = false;
    //    FrontalLobeQuizButton.image.GetComponent<Image>().color = new Color32(61, 60, 55, 100);
    //}

    void DisableStageOneOne()
    {
        FrontalLobeStageOneOneButton.enabled = false;
        FrontalLobeStageOneOneButton.image.GetComponent<Image>().color = new Color32(61, 60, 55, 100);
    }
    void EnableStageOneOne()
    {
        FrontalLobeStageOneOneButton.enabled = true;
        FrontalLobeStageOneOneButton.image.GetComponent<Image>().color = new Color32(221, 221, 221, 100);
    }

    void DisableStageOneTwo()
    {
        FrontalLobeStageOneTwoButton.enabled = false;
        FrontalLobeStageOneTwoButton.image.GetComponent<Image>().color = new Color32(61, 60, 55, 100);
    }
    void EnableStageOneTwo()
    {
        FrontalLobeStageOneTwoButton.enabled = true;
        FrontalLobeStageOneTwoButton.image.GetComponent<Image>().color = new Color32(221, 221, 221, 100);
    }

    void DisableStageOneThree()
    {
        FrontalLobeStageOneThreeButton.enabled = false;
        FrontalLobeStageOneThreeButton.image.GetComponent<Image>().color = new Color32(61, 60, 55, 100);
    }
    void EnableStageOneThree()
    {
        FrontalLobeStageOneThreeButton.enabled = true;
        FrontalLobeStageOneThreeButton.image.GetComponent<Image>().color = new Color32(221, 221, 221, 100);
    }

    void DisableStageOneFour()
    {
        FrontalLobeStageOneFourButton.enabled = false;
        FrontalLobeStageOneFourButton.image.GetComponent<Image>().color = new Color32(61, 60, 55, 100);
    }
    void EnableStageOneFour()
    {
        FrontalLobeStageOneFourButton.enabled = true;
        FrontalLobeStageOneFourButton.image.GetComponent<Image>().color = new Color32(221, 221, 221, 100);
    }

    //QUIZ
    void DisableFrontalQuiz()
    {
        FrontalLobeQuizButton.enabled = false;
        FrontalLobeQuizButton.image.GetComponent<Image>().color = new Color32(61, 60, 55, 100);
    }


    #endregion


    //DISABLE OCCIP
    void DisableOccipGame()
    {
        OccipitalLobeGameButton.enabled = false;
        OccipitalLobeGameButton.image.GetComponent<Image>().color = new Color32(61, 60, 55, 100);
    }
    void DisableOccipQuiz()
    {
        OccipitalLobeQuizButton.enabled = false;
        OccipitalLobeQuizButton.image.GetComponent<Image>().color = new Color32(61, 60, 55, 100);
    }

    //ENABLE OCCIP
    void EnableOccipGame()
    {
        OccipitalLobeGameButton.enabled = true;
        OccipitalLobeGameButton.image.GetComponent<Image>().color = new Color32(221, 221, 221, 100);
        LockOccip.SetActive(false);
    }
    void EnableOccipQuiz()
    {
        OccipitalLobeQuizButton.enabled = true;
        OccipitalLobeQuizButton.image.GetComponent<Image>().color = new Color32(221, 221, 221, 100);

        LockOccip.SetActive(false);
    }



    //DISABLE TEMPORAL
    void DisableTempGame()
    {
        TemporalLobeGameButton.enabled = false;
        TemporalLobeGameButton.image.GetComponent<Image>().color = new Color32(61, 60, 55, 100);
    }
    void DisableTempQuiz()
    {
        TemporalLobeQuizButton.enabled = false;
        TemporalLobeQuizButton.image.GetComponent<Image>().color = new Color32(61, 60, 55, 100);
    }

    //ENABLE TEMP
    void EnableTempGame()
    {
        TemporalLobeGameButton.enabled = true;
        TemporalLobeGameButton.image.GetComponent<Image>().color = new Color32(61, 60, 55, 100);
        LockTemp.SetActive(false);
    }
    void EnableTempQuiz()
    {
        TemporalLobeQuizButton.enabled = true;
        TemporalLobeQuizButton.image.GetComponent<Image>().color = new Color32(221, 221, 221, 100);

        LockBrain.SetActive(false);
    }


    //DISABLE PARIETAL
    void DisableParietalGame()
    {
        ParietalLobeGameButton.enabled = false;
        ParietalLobeGameButton.image.GetComponent<Image>().color = new Color32(61, 60, 55, 100);
    }
    void DisableParietalQuiz()
    {
        ParietalLobeQuizButton.enabled = false;
        ParietalLobeQuizButton.image.GetComponent<Image>().color = new Color32(61, 60, 55, 100);
    }

    //ENABLE PARIETAL
    void EnableParietalGame()
    {
        ParietalLobeGameButton.enabled = true;
        ParietalLobeGameButton.image.GetComponent<Image>().color = new Color32(61, 60, 55, 100);
        LockPariet.SetActive(false);
    }
    void EnableParietalQuiz()
    {
        ParietalLobeQuizButton.enabled = true;
        ParietalLobeQuizButton.image.GetComponent<Image>().color = new Color32(221, 221, 221, 100);

        LockBrain.SetActive(false);
    }

    //DISABLE CEREBELLUM
    void DisableCerebellumGame()
    {
        CerebellumGameButton.enabled = false;
        CerebellumGameButton.image.GetComponent<Image>().color = new Color32(61, 60, 55, 100);
    }
    void DisableCerebellumQuiz()
    {
        CerebellumQuizButton.enabled = false;
        CerebellumQuizButton.image.GetComponent<Image>().color = new Color32(61, 60, 55, 100);
    }

    //ENABLE CEREBELLUM
    void EnableCerebellumGame()
    {
        CerebellumGameButton.enabled = true;
        CerebellumGameButton.image.GetComponent<Image>().color = new Color32(221, 221, 221, 100);
        LockCere.SetActive(false);
    }
    void EnableCerebellumQuiz()
    {
        CerebellumQuizButton.enabled = true;
        CerebellumQuizButton.image.GetComponent<Image>().color = new Color32(221, 221, 221, 100);
    }

    //DISABLE BRAIN STEM
    void DisableBrainStemGame()
    {
        BrainStemGameButton.enabled = false;
        BrainStemGameButton.image.GetComponent<Image>().color = new Color32(61, 60, 55, 100);
    }
    void DisableBrainStemQuiz()
    {
        BrainStemQuizButton.enabled = false;
        BrainStemQuizButton.image.GetComponent<Image>().color = new Color32(61, 60, 55, 100);
    }

    //ENABLE BRAIN STEM
    void EnableBrainStemGame()
    {
        BrainStemGameButton.enabled = true;
        BrainStemGameButton.image.GetComponent<Image>().color = new Color32(221, 221, 221, 100);
        LockBrain.SetActive(false);
    }
    void EnableBrainStemQuiz()
    {
        BrainStemQuizButton.enabled = true;
        BrainStemQuizButton.image.GetComponent<Image>().color = new Color32(221, 221, 221, 100);

        LockBrain.SetActive(false);
    }


    //SCENE MANAGMENT SHIT --------------------------------------------------------------------------------------
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void LoadOneOne()
    {
        SceneManager.LoadScene("Stage1.1");
    }
}
