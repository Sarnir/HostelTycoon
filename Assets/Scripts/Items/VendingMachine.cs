using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : Item
{
    readonly int stock_max = 50;
    int stock;
    Price price;

    float baseRestockCost;

    public override void Init(ItemId itemId)
    {
        base.Init(itemId);

        price = GlobalAccess.GetAllPrices().GetPrice(PriceId.SodaCan);
        stock = stock_max;
        baseRestockCost = GlobalAccess.GetAllPrices().GetPrice(PriceId.WholesaleSodaCan).BasePrice;
    }

    public override bool Use(Person user)
    {
        if (stock > 0)
        {
            if (user.Pay(price.CurrentPrice, "using vending machine"))
            {
                stock--;

                // daj powiadomienie że trzeba napełnić automat

                return true;
            }
        }

        Debug.Log("Unsuccesful vending machine usage");
        return false;
    }

    public bool Restock(Hostel hostel)
    {
        float restockCost = baseRestockCost * (stock_max - stock);

        if (hostel.GetWallet().CanAfford(restockCost))
        {
            hostel.GetWallet().Pay(restockCost, "restocking vending machine");
            stock = stock_max;
            return true;
        }

        return false;
    }
}
