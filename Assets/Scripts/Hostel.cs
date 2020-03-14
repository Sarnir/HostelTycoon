using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hostel : MonoBehaviour
{
    [SerializeField]
    TopBar topBar = default;

    int day;

    Wallet wallet;
    
    Price pricePerNight;
    float dailyExpensesBase;

    float pointsGiven;
    int reviewsCount;

    float Rating { get { return pointsGiven / (float)reviewsCount; } }

    Property property;

    List<Guest> guestsList;
    List<Employee> staff;

    public HostelQualities Qualities { get; private set; }

    public World World;

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
        GlobalAccess.SetPricesCollection(new PricesDefinitions());

        property = new Property();
        World = FindObjectOfType<World>();

        day = 1;
        wallet = new Wallet(13000f, money => { topBar.UpdateMoneyCounter(money); });
        dailyExpensesBase = 10;
        pricePerNight = GlobalAccess.GetAllPrices().GetPrice(PriceId.BedPerNight);

        Qualities = new HostelQualities();

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

        // pracownicy pracują
        foreach(var employee in staff)
        {
            employee.Work();
        }

        // ludzie wchodzą
        int guestsCheckingIn = Random.Range(0, property.FreeBedsCount + 1);
        float guestsProfit = 0;

        for(int i = 0; i < guestsCheckingIn; i++)
        {
            Guest newGuest = new Guest(this) { LengthOfStay = Random.Range(1, 4) };

            AddGuest(newGuest);

            guestsProfit += newGuest.LengthOfStay* pricePerNight.CurrentPrice;
        }

        // ludzie robią rzeczy
        int guestsCount = Guests.Length;
        for (int i = 0; i < guestsCount; i++)
        {
            Guests[i].SpendDay();
        }

        wallet.AddMoney(guestsProfit, "guests staying");

        topBar.UpdateGuestsCounter(guestsList.Count);

        float dailyExpenses = dailyExpensesBase * guestsList.Count;

        wallet.Pay(dailyExpenses, "maintenance");

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
        Qualities.LogAllQualities();

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

        wallet.Pay(staffExpenses, "staff wages");
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
        if (wallet.CanAfford(cost))
        {
            if (property.AddNewItem(item.CreateInstance()))
            {
                wallet.Pay(cost, $"buying { item.Name }");

                Qualities.ApplyItemProperties(item.ItemProperties);

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

    public Wallet GetWallet()
    {
        return wallet;
    }
}
