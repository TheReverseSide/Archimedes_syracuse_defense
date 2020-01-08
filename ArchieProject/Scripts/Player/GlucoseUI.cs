using UnityEngine;
using UnityEngine.UI;

public class GlucoseUI : MonoBehaviour
{


    void Update()
    {
        this.gameObject.GetComponent<Text>().text = PlayerStats.currentGlucose.ToString();
    }
}
