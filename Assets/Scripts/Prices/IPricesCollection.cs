using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPricesCollection : IEnumerable
{
    void SetPrice(PriceId id, float newPrice);

    Price GetPrice(PriceId id);
}