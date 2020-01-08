using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.PostProcessing;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

public class VignettePulse : MonoBehaviour
{
    public PostProcessingProfile vignetteEffect;
    public float intensity = 0.1f;

    //public float timerDuration = 5f;
    //public bool startChanging;

    private void Update()
    {

    }

    public void startFICoR()
    {
        StartCoroutine("FadeIn");
    }

    public IEnumerator FadeIn()
    {
        for (float i = 0.02f; i < 2; i += .01f)
        {
            intensity += .02f;
            var vignette = vignetteEffect.vignette.settings;
            vignette.intensity = intensity;
            vignetteEffect.vignette.settings = vignette;

            yield return new WaitForSeconds(.08f);
        }
    }

    public void startFOCoR()
    {
        StartCoroutine("FadeOut");
    }

    public IEnumerator FadeOut()
    {
        Debug.Log("calling method");
        for (float i = 0.02f; i <= 0; i -= .02f)
        {
            intensity += .02f;
            var vignette = vignetteEffect.vignette.settings;
            vignette.intensity = intensity;
            vignetteEffect.vignette.settings = vignette;

            yield return new WaitForSeconds(.05f);
        }
    }
}
