using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hostel : MonoBehaviour
{
    public UIManager UIManager = default;

    Wallet wallet;
    
    Price pricePerNight;
    float dailyExpensesBase;

    float pointsGiven;
    int reviewsCount;

    float Rating { get { return pointsGiven / (float)reviewsCount; } }

    public World World { get; private set; }
    Inventory inventory;

    public GameTime GameTime { get; private set; }

    List<Guest> guestsList;
    List<Employee> staff;

    public HostelQualities Qualities { get; private set; }

    public Guest[] Guests
    {
        get { return guestsList.ToArray(); }
    }

    public Employee[] Staff
    {
        get { return staff.ToArray(); }
    }

    private void Awake()
    {
        if (UIManager == null)
            UIManager = FindObjectOfType<UIManager>();
    }

    private void Start()
    {
        GlobalAccess.SetItemDefinitions(new ItemDefinitions());
        GlobalAccess.SetPricesCollection(new PricesDefinitions());

        World = FindObjectOfType<World>();
        World.OnItemSpawned += ItemSpawned;

        GameTime = World.GetComponent<GameTime>();
        GameTime.OnDailyEvent += ProcessDay;

        inventory = new Inventory();
        wallet = new Wallet(13000f, money => { UIManager.UITopBar.UpdateMoneyCounter(money); });
        dailyExpensesBase = 10;
        pricePerNight = GlobalAccess.GetAllPrices().GetPrice(PriceId.BedPerNight);

        Qualities = new HostelQualities();

        guestsList = new List<Guest>();
        staff = new List<Employee>();

        UIManager.UITopBar.UpdateGuestsCounter(0);
    }

    private void Update()
    {
        Qualities[HostelQuality.Cleanliness] = World.AverageCleanliness;
    }

    private void OnDestroy()
    {
        GameTime.OnDailyEvent -= ProcessDay;
    }

    public void CheckOut(Guest guest)
    {
        // ziomek wystawia ocenę
        AddRating(guest.RateHostel());

        RemoveGuest(guest);
    }

    public void ProcessDay(DailyEvents newEvent)
    {
        switch (newEvent)
        {
            case DailyEvents.CheckOut:
                ProcessCheckOut();
                break;
            case DailyEvents.CheckIn:
                ProcessCheckIn();
                break;
            case DailyEvents.QuietHoursStart:
                ProcessQuietHoursStart();
                break;
            case DailyEvents.NewDay:
                ProcessNewDay();
                break;
            case DailyEvents.QuietHoursEnd:
                ProcessQuietHoursEnd();
                break;
            default:
                break;
        }

        // pracownicy pracują
        //foreach(var employee in staff)
        //{
        //    employee.Work();
        //}

        // ludzie robią rzeczy
        int guestsCount = Guests.Length;
        for (int i = 0; i < guestsCount; i++)
        {
            Guests[i].SpendDay();
        }
    }

    void ProcessCheckOut()
    {
        foreach (var guest in Guests)
        {
            guest.LengthOfStay--;

            if (guest.LengthOfStay < 1)
            {
                guest.CheckOut();
            }
        }

        UIManager.UITopBar.UpdateGuestsCounter(guestsList.Count);
    }

    void ProcessCheckIn()
    {
        int guestsCheckingIn = UnityEngine.Random.Range(0, inventory.FreeBedsCount + 1);
        Debug.Log("We expect " + guestsCheckingIn + " guests today");
        //float guestsProfit = 0;

        for (int i = 0; i < guestsCheckingIn; i++)
        {
            /*Guest newGuest = World.CreateGuest(this, UnityEngine.Random.Range(1, 4));
            AddGuest(newGuest);

            guestsProfit += newGuest.LengthOfStay * pricePerNight.CurrentPrice;*/

            StartCoroutine(CreateGuestAtRandomTime());
        }

        //wallet.AddMoney(guestsProfit, "guests staying");

        //topBar.UpdateGuestsCounter(guestsList.Count);
    }

    void CheckIn(Guest guest)
    {
        AddGuest(guest);

        var profit = guest.LengthOfStay * pricePerNight.CurrentPrice;
        wallet.AddMoney(profit, $"{ guest.Name } checking in");
        UIManager.UITopBar.UpdateGuestsCounter(guestsList.Count);
    }

    IEnumerator CreateGuestAtRandomTime()
    {
        float time = UnityEngine.Random.Range((float)DailyEvents.CheckIn, (float)DailyEvents.QuietHoursStart);
        while (GameTime.GetExactTime() < time)
        {
            yield return new WaitForSeconds(0.25f);
        }

        CheckIn(World.CreateGuest(this, UnityEngine.Random.Range(1, 4)));
    }

    void ProcessNewDay()
    {
        float dailyExpenses = dailyExpensesBase * guestsList.Count;

        wallet.Pay(dailyExpenses, "maintenance");

        PayStaff();

        Qualities.LogAllQualities();
    }

    void ProcessQuietHoursStart()
    {
        foreach (var guest in Guests)
        {
           // guest.wantsToSleep = true;
        }
    }

    void ProcessQuietHoursEnd()
    {
        foreach (var guest in Guests)
        {
           // guest.wantsToSleep = false;
        }
    }

    public void AddRating(float points)
    {
        pointsGiven += points;
        reviewsCount++;

        UIManager.UITopBar.UpdateRatingCounter(Rating);
    }

    void AddGuest(Guest guest)
    {
        guestsList.Add(guest);
        guest.AssignBed(inventory.FindFreeBed());
    }

    public void HireEmployee(Employee newEmployee)
    {
        staff.Add(newEmployee);
    }

    public void FireEmployee(Employee employee)
    {
        staff.Remove(employee);
        employee.Fire();
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
        guest.Bed.IsTaken = false;
    }

    public void BuyNewItem(ItemId id)
    {
        var item = GlobalAccess.GetItemDefinitions().GetDefinition(id);
        int cost = item.Price;
        if (wallet.CanAfford(cost))
        {
            AddNewItem(item);
        }
    }

    void ItemSpawned(Item item)
    {
        inventory.AddNewItem(item);
        wallet.Pay(item.Definition.Price, $"buying { item.Definition.Name }");

        Qualities.ApplyItemProperties(item.Definition.ItemProperties);
    }

    void AddNewItem(ItemDef def)
    {
        Item newItem = World.SpawnItem(def);
    }

    public Item[] GetAllItems()
    {
        return inventory.GetAllItems();
    }

    public Item FindItem(ItemId id)
    {
        return inventory.FindItem(id);
    }

    public Item[] FindItems(ItemId id)
    {
        return inventory.FindItems(id);
    }

    public Wallet GetWallet()
    {
        return wallet;
    }
}
