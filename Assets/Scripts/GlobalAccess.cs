using UnityEngine;

public class GlobalAccess
{
    static IItemDefinitions ItemDefinitions;
    
    public static void SetItemDefinitions(IItemDefinitions defs)
    {
        ItemDefinitions = defs;
    }

    public static IItemDefinitions GetItemDefinitions()
    {
        return ItemDefinitions;
    }
}