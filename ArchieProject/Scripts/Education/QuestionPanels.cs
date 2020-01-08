using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionPanels : MonoBehaviour
{
    public int questionNumber = 0;

    //private bool gotCorrect = false;
    private QuestionData question; //do i need to create a method to pass the proper array herew? Or just pass the question?

    private void FixedUpdate()
    {

    }

    void nextQuestion()
    {
        //increment through the question array (or questoin object)
    }
}
