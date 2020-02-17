using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed
{
    public bool IsTaken
    {
        get { return Guest != null; }
    }

    public Guest Guest;

    public int PricePerNight
    {
        get { return 20; }
    }
}
