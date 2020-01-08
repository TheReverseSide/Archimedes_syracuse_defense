using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour //responsible for ending the game and beating a level
{
    #region Variables
    public static bool ifGameEnded;

    PlayerStats playerStats;

    LootScript lootscript;

    [Header("Lobe specific text")]
    string frontalLobeCompletionText = " your problem-solving skills and logical reasoning has increased by ";
    string occipialLobeCompletionText = " your visual acuity has increased by ";

    [Header("Level Defeated stuff")]
    public GameObject completeLevelUI;
    public GameObject completeLevelCardList;
    public Text glucoseAmountAwarded;
    public Text glucoseAwardedText;
    public Text lobeCompleteBonusText;
    public Text quizScoreGlucoseBonus;
    public Text inGameGlucoseText;


    [Header("Synapse Orbs")]
    public Image synapseOrbOne;
    public Image synapseOrbTwo;
    public Image synapseOrbThree;
    public Image synapseOrbFour;
    public Image synapseOrbFive;

    [Header("Misc")]
    public GameObject cardHandDrop;
    public GameObject gameOverUI;


    [Header("Health bar stuff")]
    public Text lives;
    public Image healthBar;
    [SerializeField]
    private Color fullColor;
    [SerializeField]
    private Color emptyColor;
    #endregion Variables

    private void Start()
    {
        ifGameEnded = false;
        //Debug.Log("GameManager starting");

        playerStats = FindObjectOfType<PlayerStats>();

        //Fill the players hand
        if (cardHandDrop != null)
        {
            //Debug.Log("populating list");
            playerStats.PopulateHand(cardHandDrop);
        }
    }


    void Update()
    {
        if (lives != null)
        {
            lives.text = PlayerStats.Health.ToString() + "/ 100";
        }

        /*if (glucoseText != null)
        {
            glucoseText.text = PlayerStats.currentGlucose.ToString();
        }*/
        /*
        if (inGameGlucoseText != null)
        {
            inGameGlucoseText.text = PlayerStats.currentGlucose + "";
        }*/

        HandleHealthBar();

        if (Input.GetKeyDown("e")) //shortcut for ending the game when we want to test something
        {
            EndGame();
        }

        if (ifGameEnded == true) return;

        if (PlayerStats.Health >= 100) { EndGame(); }
    }

    public void LoadLevel(string levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void LoadMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void EndGame()
    {
        ifGameEnded = true;

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
    }

    void HandleHealthBar()
    {
        if (healthBar != null)
        {
            if (PlayerStats.Health == 0)
            {
                healthBar.fillAmount = (100); //if health is at zero, it shows full, otherwise.. actually divide it
            }
            else
            {
                healthBar.fillAmount = (PlayerStats.Health / 100); //gives you the percentage in the format needed (0-1)
            }

            healthBar.color = Color.Lerp(emptyColor, fullColor, healthBar.fillAmount);

        }
    }

    public void AssignStageStats(int rating, int timesRolled)
    {
        foreach (var stage in PlayerStats.stageList)
        {
            if (stage.name == SceneManager.GetActiveScene().name) //
            {
                stage.rating = rating;
                stage.beaten = true;

                //Go get the appropriate image
                //playerStats.FindGridImage(stage.name.ToString());
            }
        }

        for (int i = 0; i < timesRolled; i++)
        {
            //Roll a random chance for loot
            lootscript.CalculateLoot();
        }

        //If I wanted to keep glucose separate I could make a simple thing here that awards it
    }

    public void WinLevel()
    {
        lootscript = FindObjectOfType<LootScript>();

        //O_ this needs to be fixed for both stages and levels. Levels increase lobeIndex, but waves do not
        if (ifGameEnded == false)
        {
            ifGameEnded = true; //Freezes camera and other scripts using this variable to be active

            //Check if it is the last round

            /*
            string sceneName = SceneManager.GetActiveScene().name;
            string lastChar = sceneName.Substring(sceneName.Length - 1);
            //Debug.Log(sceneName);

            //find current lobe and see if the quiz has been completed, and add a bonus according to quiz completion
            string cutSceneName = sceneName.Substring(sceneName.Length - 2);

            if (cutSceneName.Contains("1")) //Lobe 1
            {
                foreach (var quiz in PlayerStats.quizList)
                {
                    if (quiz.name.Contains("One"))
                    {
                        if (quiz.beaten)
                        {
                            //Give bonus
                            quizScoreGlucoseBonus.text = quiz.score.ToString() + "glucose earned from completing the quiz";

                        }
                    }
                }
            }
            else if (cutSceneName.Contains("2")) //Lobe 2
            {
                foreach (var quiz in PlayerStats.quizList)
                {
                    if (quiz.name.Contains("Two"))
                    {
                        if (quiz.beaten)
                        {
                            //Give bonus
                            quizScoreGlucoseBonus.text = quiz.score.ToString() + "glucose earned from completing the quiz";
                        }
                    }
                }
            }
            else if (cutSceneName.Contains("3")) //Lobe 3
            {
                foreach (var quiz in PlayerStats.quizList)
                {
                    if (quiz.name.Contains("Three"))
                    {
                        if (quiz.beaten)
                        {
                            //Give bonus
                            quizScoreGlucoseBonus.text = quiz.score.ToString() + "glucose earned from completing the quiz";
                        }
                    }
                }
            }
            else if (cutSceneName.Contains("4")) //Lobe 4
            {
                foreach (var quiz in PlayerStats.quizList)
                {
                    if (quiz.name.Contains("Four"))
                    {
                        if (quiz.beaten)
                        {
                            //Give bonus
                            quizScoreGlucoseBonus.text = quiz.score.ToString() + "glucose earned from completing the quiz";
                        }
                    }
                }
            }

            if (lastChar.Equals("4"))//This is the last round, do something special
            {
                //Extra rewards depending on what lobe was completed
                //Push back to lobe selection screen or continue to the new stage? Give options for both. 
                if (sceneName.Equals("Scene1.4"))
                {

                }
                else if (sceneName.Equals("Scene2.4"))
                {

                }
                else if (sceneName.Equals("Scene3.4"))
                {

                }
                else if (sceneName.Equals("Scene4.4"))
                {

                }
                else if (sceneName.Equals("Scene5.4"))
                {

                }
                else if (sceneName.Equals("Scene6.4"))
                {
                    //They just beat the entire game, do something
                }

                //Marks Lobe as complete
                PlayerStats.currentLobeIndex++;

                //Gets and distributes rewards if there are vertical performance 
                SearchForRewardComb();
            }


            if (PlayerStats.Health < 5) //5 stars
            {
                synapseOrbOne.color = new Color32(255, 3, 3, 255);
                synapseOrbTwo.color = new Color32(255, 3, 3, 255);
                synapseOrbThree.color = new Color32(255, 3, 3, 255);
                synapseOrbFour.color = new Color32(255, 3, 3, 255);
                synapseOrbFive.color = new Color32(255, 3, 3, 255);

                glucoseAmountAwarded.text = "500";
                glucoseAwardedText.text = "glucose awarded";
                PlayerStats.currentGlucose += 500;

                AssignStageStats(5, 5);
                //Glucose, several cards, decent chance for rare card

                //Could just award a flat amount of glucose depending on performance and remove glucose cards? 
            }
            else if (PlayerStats.Health <= 10)//4 stars
            {
                synapseOrbOne.color = new Color32(255, 3, 3, 255);
                synapseOrbTwo.color = new Color32(255, 3, 3, 255);
                synapseOrbThree.color = new Color32(255, 3, 3, 255);
                synapseOrbFour.color = new Color32(255, 3, 3, 255);

                glucoseAmountAwarded.text = "400";
                glucoseAwardedText.text = "glucose awarded";
                PlayerStats.currentGlucose += 400;

                AssignStageStats(4, 4);
            }
            else if (PlayerStats.Health <= 20 && PlayerStats.Health > 10) //3 stars
            {
                synapseOrbOne.color = new Color32(255, 3, 3, 255);
                synapseOrbTwo.color = new Color32(255, 3, 3, 255);
                synapseOrbThree.color = new Color32(255, 3, 3, 255);

                glucoseAmountAwarded.text = "250";
                glucoseAwardedText.text = "glucose awarded";
                PlayerStats.currentGlucose += 250;

                AssignStageStats(3, 3);
                //Glucose, a few cards
            }
            else if (PlayerStats.Health <= 30 && PlayerStats.Health > 20) //2 stars
            {
                synapseOrbOne.color = new Color32(255, 3, 3, 255);
                synapseOrbTwo.color = new Color32(255, 3, 3, 255);

                glucoseAmountAwarded.text = "200";
                glucoseAwardedText.text = "glucose awarded";
                PlayerStats.currentGlucose += 200;

                AssignStageStats(2, 2);
                //Glucose, a card?
            }
            else //1 star
            {
                synapseOrbOne.color = new Color32(255, 3, 3, 255);

                glucoseAmountAwarded.text = "100";
                glucoseAwardedText.text = "glucose awarded";
                PlayerStats.currentGlucose += 100;

                AssignStageStats(1, 1);
                //Just glucose
            }
            */

            if (completeLevelUI != null)
            {
                completeLevelUI.SetActive(true);
            }
        }
        //Fill out endLevel Text
        List<GameObject> rewardList = lootscript.sendBackRewards();

        foreach (var reward in rewardList)
        {
            Instantiate(reward.gameObject, completeLevelCardList.transform);
        }
    }

    void SearchForRewardComb()
    {
        //This should evaluate performance of previous stages to see if vertical bars are aligned for some bonus +  give brain improvement text
        int ratingsTotal = 0;

        foreach (var stage in PlayerStats.stageList)
        {
            ratingsTotal += stage.rating;

            //"Due to your perfect performance, your brain connectivity has drasticalliy increased, resulting in a large boost in visual acuity!"
            //THIS ALSO NEEDS TO TAKE INTO ACCOUNT QUIZ PERFORMANCE, OTHERWISE IT IS USELESS - Should also be sent to levelCompleted screen
        }

        if (ratingsTotal >= 20) //5 stars each average
        {
            //Connect to .4 stage levelBeaten screens

            lobeCompleteBonusText.text = "Due to your perfect performance, the infection has been completely elminated your frontal lobe, and your roblem-solving skills and logical reasoning has increased by 50%. Great job!"; //needs to go and find appropriate lobe and associated text
        } //fOR THE DEMO, I THINK HARDODING THIS IS FINE. FIX IT AFTER THE APPLICATION.
        else if (ratingsTotal >= 16) //4 stars each average
        {
            lobeCompleteBonusText.text = "Due to your exceptional performance, the infection only created very minor damage your frontal lobe, and your problem-solving skills and logical reasoning has increased by 30%. Great job!";

        }
        else if (ratingsTotal >= 12) //3 stars each average
        {
            lobeCompleteBonusText.text = "Due to your good performance.."; //ADD LATER
        }
        else if (ratingsTotal >= 8) //2 stars each average
        {
            lobeCompleteBonusText.text = "";  //ADD LATER
        }
        else if (ratingsTotal >= 4) //1 stars each average
        {
            lobeCompleteBonusText.text = "";  //ADD LATER
        }
    }

}
