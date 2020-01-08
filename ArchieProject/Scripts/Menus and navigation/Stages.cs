using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stages //I removed monobehavior because I wont be attaching it to anything (at the moment)
{
    //A stage object for each stage in game, should indiciate its name, its number (position), and whether it has been beaten, and its rating

    public Stages(string name, int position, int rating, bool beaten, string nextStage)
    {
        this.name = name;
        this.position = position;
        this.rating = rating;
        this.beaten = beaten;
        this.nextStage = nextStage;
    }

    public string name { get; set; }
    public int position { get; set; }
    public int rating { get; set; }
    public bool beaten { get; set; }
    public string nextStage { get; set; }


}
