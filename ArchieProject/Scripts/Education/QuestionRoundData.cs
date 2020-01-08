using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestionRoundData
{
    public string questionRoundName;
    public int questionValue = 100;

    [Header("Questions")]
    public QuestionData[] questions;
}
