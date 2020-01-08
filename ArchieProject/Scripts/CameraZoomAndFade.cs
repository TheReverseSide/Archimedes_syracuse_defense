using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraZoomAndFade : MonoBehaviour
{
    string sceneToLoad;
    float animDuration;

    public float animCutTime;

    Animator anim;

    VignettePulse vignettePulse;

    private void Start()
    {
        sceneToLoad = "CameraTransitionScene";

        anim = this.GetComponent<Animator>();
        vignettePulse = this.gameObject.GetComponent<VignettePulse>();
    }

    public void PlayAnimation()
    {
        anim.Play("DropDownBeneathLevel");

        vignettePulse.startFICoR();

        StartCoroutine("ShowCurrentClipLength");
    }

    IEnumerator ShowCurrentClipLength()
    {
        yield return new WaitForEndOfFrame();


        Debug.Log("should get length here");
        animDuration = anim.GetCurrentAnimatorStateInfo(0).length - animCutTime;

        Debug.Log("anim duration: " + animDuration);

        StartCoroutine("LoadTransitionScene");
    }

    IEnumerator LoadTransitionScene()
    {
        yield return new WaitForSeconds(animDuration);

        vignettePulse.startFOCoR();

        SceneManager.LoadScene(sceneToLoad);
    }
}
