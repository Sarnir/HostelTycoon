using System.Collections.Generic;
using UnityEngine;

public class GlobalAccess
{
    static IItemDefinitions ItemDefinitions;
    static IPricesCollection PricesCollection;
    
    public static void SetItemDefinitions(IItemDefinitions defs)
    {
        ItemDefinitions = defs;
    }

    public static IItemDefinitions GetItemDefinitions()
    {
        return ItemDefinitions;
    }

    public static void SetPricesCollection(IPricesCollection collection)
    {
        PricesCollection = collection;
    }

    public static IPricesCollection GetAllPrices()
    {
        return PricesCollection;
    }
}