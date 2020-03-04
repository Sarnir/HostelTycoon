using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guest: Person
{
    public int LengthOfStay;
    public int SatisfactionLvl;

    public int BedNo;

    public Guest(Hostel hostel) : base(hostel)
    {
        SatisfactionLvl = 1;
    }

    public void SpendDay()
    {
        // typek jest thirsty i chce mu się pić
        if (Random.value > 0.5f)
        {
            var vendingMachine = hostel.FindItem(ItemId.VendingMachine);
            if(vendingMachine != null)
            {
                if (vendingMachine.Use(this))
                    SatisfactionLvl++;
            }
        }

        // typek korzysta z toalety <- docelowo będzie odpowiedni item i interakcja z nim
        hostel.Cleanliness -= Random.Range(0.01f, 0.04f);
        if (hostel.Cleanliness < 0f)
            hostel.Cleanliness = 0f;
    }

    public float RateHostel()
    {
        float rating = Mathf.Clamp((Random.Range(4, 10) * hostel.Cleanliness) + SatisfactionLvl, 1, 10);
        Debug.Log($"{ Name }'s rating is { rating }");
        return rating;
    }
}
