﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed: Item
{
    public bool IsTaken
    {
        get { return Guest != null; }
    }

    // łóżko piętrowe może pomieścić dwóch gości, ale pojedyncze już jednego.
    // łóżko podwójne może zmieścić dwie osoby, ale tylko takie które przyszły razem.
    public Guest Guest;

    public int BedNo;

    public Bed(int id): base(id)
    {
    }

    public override void Interact()
    {
        // zmniejsz czystość?
        // rzuć kością na pluskwy xD
    }
}