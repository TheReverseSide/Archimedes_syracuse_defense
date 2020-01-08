using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quiz : MonoBehaviour
{

    public Quiz(string name, int position, float score, bool beaten, string nextStage)
    {
        this.name = name;
        this.position = position;
        this.score = score;
        this.beaten = beaten;
        this.nextStage = nextStage;
    }

    public string name { get; set; }
    public int position { get; set; }
    public float score { get; set; }
    public bool beaten { get; set; }
    public string nextStage { get; set; }
}
