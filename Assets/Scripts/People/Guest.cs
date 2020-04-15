using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guest : Person
{
    public int LengthOfStay;
    int SatisfactionLvl;

    public Bed Bed { get { return bedAssigned; } }

    Bed bedAssigned;

    public override void Init(Hostel hostel, PersonData pData)
    {
        base.Init(hostel, pData);

        SatisfactionLvl = 1;
    }

    protected override void Update()
    {
        base.Update();

        

        /*if(isSleeping)
        {
            sleepingNeed -= (sleepingNeedGrowthRate * Time.timeScale * 2f);
            if (sleepingNeed <= 0f)
                WakeUp();
        }
        else
        {
            sleepingNeed += sleepingNeedGrowthRate * Time.timeScale;
            if (sleepingNeed >= 1f)
                GoToSleep();
        }*/
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
        hostel.Qualities.ModifyQuality(HostelQuality.Cleanliness, Random.Range(-0.03f, 0f));
    }

    public void AssignBed(Bed bed)
    {
        bedAssigned = bed;
        bed.IsTaken = true;
    }

    public void CheckOut()
    {
        hostel.CheckOut(this);
        Destroy(gameObject);
    }

    public float RateHostel()
    {
        float rating = Mathf.Clamp((Random.Range(4, 10) * hostel.Qualities[HostelQuality.Cleanliness]) + SatisfactionLvl, 1, 10);
        Debug.Log($"{ data.Name }'s rating is { rating.ToString("0.00") }");
        return rating;
    }
}
