using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hostel : MonoBehaviour
{
    // inventory
    // ilość pokoi
    // ilość łóżek
    // ilość kasy
    // ilość gości
    // ceny za łóżka
    // wydatki

    int day;

    int money;
    
    int bedBasePrice;

    int pricePerNight;
    int dailyExpenses;

    Property property;

    List<Guest> guestsList;

    public Guest[] Guests
    {
        get { return guestsList.ToArray(); }
    }

    public Text DayCounter;
    public Text MoneyCounter;
    public Text GuestsCounter;
    public Text BedsCounter;
    public Text BedPriceText;

    private void Start()
    {
        property = new Property();

        day = 1;
        money = 1000;
        dailyExpenses = 10;
        pricePerNight = 30;

        bedBasePrice = 50;

        guestsList = new List<Guest>();

        DayCounter.text = "Day " + day;
        UpdateUI();

        UpdateBedPrice();
    }

    public void ProcessDay()
    {
        // ludzie wychodzą
        foreach(var guest in Guests)
        {
            guest.LengthOfStay--;

            if (guest.LengthOfStay < 1)
            {
                RemoveGuest(guest);
            }
        }

        // ludzie wchodzą
        int guestsCheckingIn = Random.Range(0, property.BedQuantity-guestsList.Count);

        for(int i = 0; i < guestsCheckingIn; i++)
        {
            Guest newGuest = new Guest() { LengthOfStay = Random.Range(1, 4) };

            AddGuest(newGuest);

            money += newGuest.LengthOfStay* pricePerNight;
        }

        GuestsCounter.text = "Guests: " + guestsList.Count;

        money -= dailyExpenses * guestsList.Count;

        UpdateMoneyCounter();

        day++;
        DayCounter.text = "Day " + day;
    }

    void AddGuest(Guest guest)
    {
        guestsList.Add(guest);
        property.GetBeds().Find(x => x.IsTaken == false).Guest = guest;
    }

    void RemoveGuest(Guest guest)
    {
        guestsList.Remove(guest);
        property.ClearBed(guest);
    }

    public void BuyNewBed()
    {
        int cost = bedBasePrice * property.BedQuantity;
        if (money >= cost)
        {
            if (property.AddNewBed())
            {
                money -= cost;
                UpdateUI();
            }
        }
    }

    public void ShowGuestList()
    {
        var beds = property.GetBeds();
        foreach(var bed in beds)
        {
            if(bed.IsTaken)
            {
                Debug.Log("Guest " + bed.Guest.Name + ", staying for " + bed.Guest.LengthOfStay + " nights");
            }
        }
    }

    public void GetGuestsList()
    {
        //property.
    }

    void UpdateMoneyCounter()
    {
        MoneyCounter.text = money + "$";
    }

    void UpdateBedCounter()
    {
        BedsCounter.text = "Beds: " + property.BedQuantity + "/" + property.BedLimit;
    }

    void UpdateBedPrice()
    {
        BedPriceText.text = "Buy bed for " + (bedBasePrice * property.BedQuantity);
    }

    void UpdateUI()
    {
        UpdateMoneyCounter();
        UpdateBedCounter();
        UpdateBedPrice();
    }
}
