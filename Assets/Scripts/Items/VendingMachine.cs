using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : Item
{
    readonly int stock_max = 50;
    int stock;
    float price;

    public VendingMachine(ItemId id): base(id)
    {
        price = 1f;
        stock = stock_max;
        Debug.Log("Vending machine of id " + id.ToString() + " created");
    }

    public override bool Use(Person user)
    {
        if (stock > 0)
        {
            if (user.Pay(price))
            {
                Debug.Log("Vending Machine USED");
                stock--;

                // daj powiadomienie że trzeba napełnić automat

                return true;
            }
        }

        Debug.Log("Unsuccesful vending machine usage");
        return false;
    }

    public void SetPrice(float newPrice)
    {
        price = newPrice;
    }

    public bool Restock(Hostel hostel)
    {
        //if(hostel.buyStock())
        stock = stock_max;

        return true;
    }
}
