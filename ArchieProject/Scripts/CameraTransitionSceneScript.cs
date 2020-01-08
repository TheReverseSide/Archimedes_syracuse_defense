using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraTransitionSceneScript : MonoBehaviour
{
    Animator anim;
    float animDuration;

    string sceneToLoad;

    //When called needs to start the CameraTranstionSceneDrop animation

    private void Start()
    {
        anim = this.GetComponent<Animator>();
        anim.Play("CameraTranstionSceneDrop");

        //foreach (var stage in PlayerStats.stageList)
        //{
        //    if (stage.beaten == true)
        //    {
        //        sceneToLoad = stage.nextStage;
        //    }
        //    else //TEST
        //    {

        //    }
        //}

        sceneToLoad = (PlayerStats.levelDIctionary[PlayerStats.farthestStage + 1].ToString());

        StartCoroutine("ShowCurrentClipLength");
    }

    IEnumerator ShowCurrentClipLength()
    {
        yield return new WaitForEndOfFrame();
        animDuration = anim.GetCurrentAnimatorStateInfo(0).length;

        Debug.Log(animDuration);

        StartCoroutine("LoadFollowingScene");
    }
    //After the animatin is complete, load the following scene (probably passed to it by something)

    IEnumerator LoadFollowingScene()
    {

        Debug.Log("entered coroutine to load");
        yield return new WaitForSeconds(animDuration);
        Debug.Log("done waiting- " + sceneToLoad);



        if (sceneToLoad != null)
        {
            Debug.Log("Loading next scene...");
            SceneManager.LoadScene(sceneToLoad);
        }


    }
}
