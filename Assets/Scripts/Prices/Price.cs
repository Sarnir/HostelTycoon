using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Price
{
    public PriceId Id { get; private set; }
    public float BasePrice { get; private set; }
    float price;

    public float CurrentPrice { get { return price; } set { price = value; } }

    public float Profit { get { return price - BasePrice; } }
    public string ProfitString { get { return $"{Profit.ToString("0.00")}$"; } }

    public bool Modifiable { get; private set; }

    public bool IsUsed { get; set; }

    public Price(PriceId _id, float _base, bool modifiable)
    {
        Id = _id;
        BasePrice = _base;
        price = _base;
        Modifiable = modifiable;
    }

    public override string ToString()
    {
        return $"{price.ToString("0.00")}$";
    }
}