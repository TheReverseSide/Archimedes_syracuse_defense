using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using SimpleJSON;

public class LoginRegister : MonoBehaviour
{
    public GameObject login_object;
    public GameObject register_object;
    public GameObject intro_object;

    public Text errorTextLogin;
    public Text errorTextRegister;

    public UnityEngine.UI.InputField input_login_username;

    //private string gameDataFilePath;
    //private bool loggedIn;
    private int part = 0;

    private void Start()
    {
        //gameDataFilePath = Application.dataPath + "/StreamingAssets/data.json";
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.S)) Save();

        if (part == 0) //Inital page
        {
            //intro_object.gameObject.SetActive(true);
            login_object.gameObject.SetActive(true);
            register_object.gameObject.SetActive(false);
        }
        if (part == 1)//Hiding intro
        {
            //intro_object.gameObject.SetActive(false);
            login_object.gameObject.SetActive(true);
            register_object.gameObject.SetActive(false);
        }
        if (part == 2)//Needs to register, load that screen
        {
            //intro_object.gameObject.SetActive(false);
            login_object.gameObject.SetActive(false);
            register_object.gameObject.SetActive(true);
        }
    }

    public void HideIntro()
    {
        part = 1;
    }

    public void LoadMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadRegister()
    {
        part = 2;
    }

    //public void Register()
    //{
    //    //check fields aren't blank
    //    if (input_login_username.text != "")
    //    {
    //        PlayerStats.Name = input_login_username.text; //Can I create a JSON file based off this name?
    //        Save();
    //        Debug.Log(PlayerStats.Name);
    //    }
    //    else errorTextRegister.text = "Error: Incorrect username format";
    //}

    //public void SubmitLogin() //Try to find JSON with that name - Make an array, then check for presence
    //{
    //    string userName = input_login_username.text;

    //    foreach (JSONObject file in Directory.GetFiles(Application.dataPath + gameDataFilePath)) //persistentDataPath
    //    {
    //        if (file.ToString().Contains(userName))
    //        {
    //            //Load() and return;
    //            Debug.Log("found it!");
    //        }
    //    }

    //    errorTextLogin.text = "This username could not be found";
    //}

    //public void LogOut()
    //{
    //    //loggedIn_Username = "";

    //    //loggedIn = false; //does this actually remove playerPrefs?

    //    Debug.Log("Logged out");

    //    SceneManager.LoadScene("LoginMenu");
    //    intro_object.gameObject.SetActive(false);
    //    login_object.gameObject.SetActive(true);
    //    register_object.gameObject.SetActive(false);
    //}

    public void PlayGuest()
    {
        PlayerStats.isGuest = true;
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Debug.Log("Exiting...");
        Application.Quit();
    }

    //public void Save() //Creates/saves Json
    //{
    //    JSONObject playerJson = new JSONObject();
    //    playerJson.Add("Name", PlayerStats.Name);
    //    playerJson.Add("Lives", PlayerStats.Health);
    //    //Add the rest

    //    string path = Application.persistentDataPath + "/PlayerSave.json";
    //    File.WriteAllText(path, playerJson.ToString());

    //    Debug.Log(playerJson.ToString());

    //    // you can use JSONArray position 0 new JsonArray to store location as: position.add(transform.position.x); repeat for y and z
    //    //PlayerJson.add("position", position)
    //}

    //void Load()
    //{
    //    string jsonString = File.ReadAllText(gameDataFilePath);
    //    JSONObject playerJson = (JSONObject)JSON.Parse(jsonString);

    //    //set values
    //    PlayerStats.Name = playerJson["Name"];
    //    PlayerStats.Health = playerJson["Lives"];
    //    //Add the rest

    //    //Position: 
    //    /*transform.position = new Vector3(
    //        playerJson["Position"].AsArray[0],
    //        playerJson["Position"].AsArray[1],
    //        playerJson["Position"].AsArray[2]); */
    //}

    //public void ClearUsers()
    //{
    //    foreach (JSONObject file in Directory.GetFiles(Application.persistentDataPath))
    //    {
    //        File.Delete(gameDataFilePath);
    //    }
    //}

}
