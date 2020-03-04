using UnityEngine;

public enum ItemId
{
    CheapBed = 0,
    LuxuryBed,
    BunkBed,
    PoolTable,
    VendingMachine
}

public class ItemDefinitions: IItemDefinitions
{
    ItemDef[] definitions;

    public ItemDefinitions()
    {
        Initialize();
    }

    void Initialize()
    {
        definitions = new ItemDef[]
        {
            new ItemDef(ItemId.CheapBed, "Cheap Bed", 400, 6, "bedCheap"),
            new ItemDef(ItemId.LuxuryBed, "Luxury Bed", 800, 6, "bedLuxury"),
            new ItemDef(ItemId.BunkBed, "Bunk Bed", 600, 6, "bedBunk"),
            new ItemDef(ItemId.PoolTable, "Pool Table", 1000, 6, "poolTable"),
            new ItemDef(ItemId.VendingMachine, "Vending Machine", 1200, 2, "vending")
        };
    }

    public ItemDef GetDefinition(ItemId id)
    {
        return definitions[(int)id];
    }

    public ItemDef[] GetAllDefinitions()
    {
        return definitions.Clone() as ItemDef[];
    }
}