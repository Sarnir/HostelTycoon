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
        hostel.Qualities.ModifyQuality(HostelQuality.Cleanliness, Random.Range(-3, 0));
    }

    public float RateHostel()
    {
        float rating = Mathf.Clamp((Random.Range(4, 10) * hostel.Qualities[HostelQuality.Cleanliness] * 0.01f) + SatisfactionLvl, 1, 10);
        Debug.Log($"{ Name }'s rating is { rating.ToString("0.00") }");
        return rating;
    }
}
