using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    public Image image;
    public AnimationCurve fadeCurve;

    void Start() { StartCoroutine(FadeIn()); }

    public void FadeTo(string scene) { StartCoroutine(FadeOut(scene)); }

    IEnumerator FadeIn()
    {
        float tm = 1f; //We want to animate from 1 to 0

        while (tm > 0) //while there is still time, do...
        {
            tm -= Time.deltaTime; //Could multiply by some speed value to manipulate how long it takes 
            float a = fadeCurve.Evaluate(tm); //Create a value for the alpha and sets it according to our tm, evaluted by animationCurve
            image.color = new Color(0, 0, 0, a); //Creating new color and setting alpha to time value
            yield return 0; //Wait until the next frame and then continue so it doesnt go to 0 immediately
        }
    }

    IEnumerator FadeOut(string scene)
    {
        float tm = 0f; //We want to animate from 0 to 1

        while (tm <= 1f)
        {
            tm += Time.deltaTime;
            float a = fadeCurve.Evaluate(tm);
            image.color = new Color(0, 0, 0, a);
            yield return 0;
        }

        SceneManager.LoadScene(scene);
    }
}
