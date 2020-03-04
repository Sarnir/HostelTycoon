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

    [SerializeField]
    TopBar topBar = default;

    int day;

    float _money;
    float money { get { return _money; } set { _money = value; topBar.UpdateMoneyCounter(_money); } }
    
    int pricePerNight;
    int dailyExpensesBase;

    float pointsGiven;
    int reviewsCount;

    float Rating { get { return pointsGiven / (float)reviewsCount; } }

    Property property;

    List<Guest> guestsList;
    List<Employee> staff;

    public float Cleanliness;

    public delegate void DateEvent();
    public event DateEvent OnNewWeek;

    public Guest[] Guests
    {
        get { return guestsList.ToArray(); }
    }

    public Employee[] Staff
    {
        get { return staff.ToArray(); }
    }

    private void Start()
    {
        GlobalAccess.SetItemDefinitions(new ItemDefinitions());

        property = new Property();

        day = 1;
        money = 3000;
        dailyExpensesBase = 10;
        pricePerNight = 30;
        Cleanliness = 1;

        guestsList = new List<Guest>();
        staff = new List<Employee>();

        topBar.UpdateDayCounter(day);
        topBar.UpdateGuestsCounter(0);
        topBar.UpdateSpaceCounter(property.CurrentSpace, property.TotalSpace);
    }

    public void ProcessDay()
    {
        // ludzie wychodzą
        foreach(var guest in Guests)
        {
            guest.LengthOfStay--;

            if (guest.LengthOfStay < 1)
            {
                // ziomek wystawia ocenę
                AddRating(guest.RateHostel());

                RemoveGuest(guest);
            }
        }

        // ludzie wchodzą
        int guestsCheckingIn = Random.Range(0, property.FreeBedsCount + 1);
        int guestsProfit = 0;

        for(int i = 0; i < guestsCheckingIn; i++)
        {
            Guest newGuest = new Guest(this) { LengthOfStay = Random.Range(1, 4) };

            AddGuest(newGuest);

            guestsProfit += newGuest.LengthOfStay* pricePerNight;
        }

        // ludzie robią rzeczy
        int guestsCount = Guests.Length;
        for (int i = 0; i < guestsCount; i++)
        {
            Guests[i].SpendDay();
        }

        LogIncome(guestsProfit, "guests staying");
        money += guestsProfit;

        topBar.UpdateGuestsCounter(guestsList.Count);

        int dailyExpenses = dailyExpensesBase * guestsList.Count;

        LogExpenses(dailyExpenses, "maintenance");
        money -= dailyExpenses;

        PayStaff();

        NextDay();
    }

    public void AddRating(float points)
    {
        pointsGiven += points;
        reviewsCount++;

        topBar.UpdateRatingCounter(Rating);
    }

    void AddGuest(Guest guest)
    {
        guestsList.Add(guest);
        var bed = property.FindFreeBed();
        guest.BedNo = bed.BedNo;
        bed.Guest = guest;
    }

    void NextDay()
    {
        day++;
        topBar.UpdateDayCounter(day);

        if(day%7 == 1)
        {
            OnNewWeek?.Invoke();
        }
    }

    public void HireEmployee(Employee newEmployee)
    {
        staff.Add(newEmployee);
    }

    public void FireEmployee(Employee employee)
    {
        staff.Remove(employee);
    }

    void PayStaff()
    {
        int staffExpenses = 0;
        foreach(var employee in staff)
        {
            staffExpenses += employee.Wage;
        }

        LogExpenses(staffExpenses, "staff wages");
        money -= staffExpenses;
    }

    void LogExpenses(int expenses, string type)
    {
        Debug.Log($"<color=red>Paid {expenses}$ for {type}</color>");
    }

    void LogIncome(int income, string type)
    {
        Debug.Log($"<color=green>Got {income}$ from {type}</color>");
    }

    void RemoveGuest(Guest guest)
    {
        guestsList.Remove(guest);
        property.ClearBed(guest.BedNo);
    }

    public void BuyNewItem(ItemId id)
    {
        var item = GlobalAccess.GetItemDefinitions().GetDefinition(id);
        int cost = item.Price;
        if (money >= cost)
        {
            if (property.AddNewItem(item.CreateInstance()))
            {
                money -= cost;
                topBar.UpdateSpaceCounter(property.CurrentSpace, property.TotalSpace);
            }
        }
    }

    public Item[] GetAllItems()
    {
        return property.GetAllItems();
    }
    public Item FindItem(ItemId id)
    {
        return property.FindItem(id);
    }

    public Item[] FindItems(ItemId id)
    {
        return property.FindItems(id);
    }
}
