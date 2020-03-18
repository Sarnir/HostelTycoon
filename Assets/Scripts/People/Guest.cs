
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guest : Person
{
    public int LengthOfStay;
    int SatisfactionLvl;

    public Bed Bed { get { return bedAssigned; } }

    Bed bedAssigned;

    [SerializeField]
    float sleepingNeed = 0f;

    [SerializeField]
    float sleepingNeedGrowthRate = 0.00013f;

    bool isSleeping = false;

    public override void Init(Hostel hostel, PersonData pData)
    {
        base.Init(hostel, pData);

        SatisfactionLvl = 1;
        animator.SetFloat("Offset", Random.value);

        sleepingNeed = 0.4f + 0.2f * Random.value;
    }

    private void Update()
    {
        if(isSleeping)
        {
            sleepingNeed -= (sleepingNeedGrowthRate * 2f);
            if (sleepingNeed <= 0f)
                WakeUp();
        }
        else
        {
            sleepingNeed += sleepingNeedGrowthRate;
            if (sleepingNeed >= 1f)
                GoToSleep();
        }
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

    public void AssignBed(Bed bed)
    {
        bedAssigned = bed;
        bed.IsTaken = true;
    }

    void GoToSleep()
    {
        isSleeping = true;
        animator.SetBool("IsSleeping", true);
        
        bedAssigned.Use(this);
    }

    void WakeUp()
    {
        isSleeping = false;
        animator.SetBool("IsSleeping", false);
        transform.position = new Vector3(UnityEngine.Random.Range(-4, 4), UnityEngine.Random.Range(-4, 4), 0);
    }

    public void CheckOut()
    {
        hostel.CheckOut(this);
        Destroy(gameObject);
    }

    public float RateHostel()
    {
        float rating = Mathf.Clamp((UnityEngine.Random.Range(4, 10) * hostel.Qualities[HostelQuality.Cleanliness] * 0.01f) + SatisfactionLvl, 1, 10);
        Debug.Log($"{ data.Name }'s rating is { rating.ToString("0.00") }");
        return rating;
    }
}
