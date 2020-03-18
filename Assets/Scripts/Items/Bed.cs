using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : Item
{
    public bool IsTaken;

    // łóżko piętrowe może pomieścić dwóch gości, ale pojedyncze już jednego.
    // łóżko podwójne może zmieścić dwie osoby, ale tylko takie które przyszły razem.
    //public Guest Guest; <- cross reference :o)

    public override bool Use(Person user)
    {
        base.Use(user);
        // zmniejsz czystość?
        // rzuć kością na pluskwy xD

        //if (user is Employee)
        //    Clear();
        if (user is Guest)
        {
            Debug.Log($"{user.Name} went to sleep!");
            user.transform.position = transform.position;
        }

        return true;
    }

    public void Prepare()
    {
        IsTaken = false;
    }
}
