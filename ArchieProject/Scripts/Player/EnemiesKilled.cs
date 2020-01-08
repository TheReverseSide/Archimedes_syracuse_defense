using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesKilled : MonoBehaviour
{
    public Text enemiesText;

    private void OnEnable()
    {
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        int enemies = 0; //starting enemies
        enemiesText.text = "0"; //Making sure that the text starts at zero

        yield return new WaitForSeconds(.7f); //adding a delay for our fade in animation

        while (enemies < PlayerStats.enemiesKilled) //PlayerStats.rounds
        {
            enemies++;
            enemiesText.text = enemies.ToString();

            yield return new WaitForSeconds(.05f); //adding small delay so we can actually see it
        }
    }

}
