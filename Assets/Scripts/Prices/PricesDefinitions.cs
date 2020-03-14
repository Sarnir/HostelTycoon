using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PriceId
{
    BedPerNight,
    SodaCan,
    WholesaleSodaCan
}

public class PricesDefinitions : IPricesCollection
{
    Dictionary<PriceId, Price> dict;

    public PricesDefinitions()
    {
        Initialize();
    }

    void Initialize()
    {
        dict = new Dictionary<PriceId, Price>()
        {
            { PriceId.BedPerNight, new Price(PriceId.BedPerNight, 30f, true) },
            { PriceId.SodaCan, new Price(PriceId.SodaCan, 2f, true) },
            { PriceId.WholesaleSodaCan, new Price(PriceId.WholesaleSodaCan, 0.4f, false) }
        };
    }

    public Price GetPrice(PriceId id)
    {
        dict[id].IsUsed = true;

        return dict[id];
    }

    public void SetPrice(PriceId id, float newPrice)
    {
        dict[id].CurrentPrice = newPrice;
    }

    public IEnumerator GetEnumerator()
    {
        return dict.Values.GetEnumerator();
    }
}