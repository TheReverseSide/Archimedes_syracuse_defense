using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TotalGlucoseEarned : MonoBehaviour
{

    public Text glucoseText;

    private void OnEnable()
    {
        //StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        int glucose = 0; //starting glucose
        glucoseText.text = "0"; //Making sure that the text starts at zero

        yield return new WaitForSeconds(.7f); //adding a delay for our fade in animation

        while (glucose < PlayerStats.currentGlucose)
        {
            glucose++;
            glucoseText.text = glucose.ToString();

            yield return new WaitForSeconds(.0005f); //adding small delay so we can actually see it
        }
    }

}
