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
    public Text SpaceCounter;
    public Text BedPriceText;

    private void Start()
    {
        GlobalAccess.SetItemDefinitions(new ItemDefinitions());

        property = new Property();

        day = 1;
        money = 1000;
        dailyExpenses = 10;
        pricePerNight = 30;

        guestsList = new List<Guest>();

        DayCounter.text = "Day " + day;
        UpdateUI();
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
        int guestsCheckingIn = Random.Range(0, property.FreeBedsCount + 1);

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
        var bed = property.FindFreeBed();
        guest.BedNo = bed.BedNo;
        bed.Guest = guest;
    }

    void RemoveGuest(Guest guest)
    {
        guestsList.Remove(guest);
        property.ClearBed(guest.BedNo);
    }

    public void BuyNewBed()
    {
        var bed = GlobalAccess.GetItemDefinitions().GetDefinition(0);
        int cost = bed.Price;
        if (money >= cost)
        {
            if (property.AddNewItem(new Bed(0)))
            {
                money -= cost;
                UpdateUI();
            }
        }
    }

    public void BuyNewItem(int id)
    {
        var item = GlobalAccess.GetItemDefinitions().GetDefinition(id);
        int cost = item.Price;
        if (money >= cost)
        {
            if (property.AddNewItem(item.CreateInstance()))
            {
                money -= cost;
                UpdateUI();
            }
        }
    }

    public Item[] GetAllItems()
    {
        return property.GetAllItems();
    }

    void UpdateMoneyCounter()
    {
        MoneyCounter.text = money + "$";
    }

    void UpdateSpaceCounter()
    {
        SpaceCounter.text = "Space: " + property.CurrentSpace + "/" + property.TotalSpace;
    }

    void UpdateUI()
    {
        UpdateMoneyCounter();
        UpdateSpaceCounter();
    }
}
