using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guest : Person
{
    public int LengthOfStay;
    public int SatisfactionLvl;

    public int BedNo;

    public override void Init(Hostel hostel, PersonData pData)
    {
        base.Init(hostel, pData);

        SatisfactionLvl = 1;
    }

    public void SpendDay()
    {
        // typek jest thirsty i chce mu się pić
        if (UnityEngine.Random.value > 0.5f)
        {
            var vendingMachine = hostel.FindItem(ItemId.VendingMachine);
            if(vendingMachine != null)
            {
                if (vendingMachine.Use(this))
                    SatisfactionLvl++;
            }
        }

        // typek korzysta z toalety <- docelowo będzie odpowiedni item i interakcja z nim
        hostel.Qualities.ModifyQuality(HostelQuality.Cleanliness, UnityEngine.Random.Range(-3, 0));
    }

    public float RateHostel()
    {
        float rating = Mathf.Clamp((UnityEngine.Random.Range(4, 10) * hostel.Qualities[HostelQuality.Cleanliness] * 0.01f) + SatisfactionLvl, 1, 10);
        Debug.Log($"{ data.Name }'s rating is { rating.ToString("0.00") }");
        return rating;
    }

    public static Guest Create(Hostel hostel, int lengthOfStay)
    {
        Guest sprite = Instantiate(Resources.Load<Guest>($"Prefabs/People/LillyGuest"));
        sprite.Init(hostel, new PersonData());
        sprite.LengthOfStay = lengthOfStay;

        return sprite;
    }
}
