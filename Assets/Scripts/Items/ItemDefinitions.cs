using UnityEngine;

public enum ItemId
{
    SpaceExtension = 0,
    CheapBed,
    LuxuryBed,
    BunkBed,
    PoolTable,
    VendingMachine,
    RZUTKIxd
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
            new ItemDef(ItemId.SpaceExtension, "Space Extension", 1000, 0, "bedCheap", null),
            new ItemDef(ItemId.CheapBed, "Cheap Bed", 400, 6, "bedCheap", "CheapBed", new ItemProperty[] { new ItemProperty(HostelQuality.Comfort, -1) }),
            new ItemDef(ItemId.LuxuryBed, "Luxury Bed", 800, 6, "bedLuxury", null, new ItemProperty[] { new ItemProperty(HostelQuality.Comfort, 1) }),
            new ItemDef(ItemId.BunkBed, "Bunk Bed", 600, 6, "bedBunk", null),
            new ItemDef(ItemId.PoolTable, "Pool Table", 1000, 6, "poolTable", "PoolTable", new ItemProperty[] { new ItemProperty(HostelQuality.Facilities, 1) }),
            new ItemDef(ItemId.VendingMachine, "Vending Machine", 1200, 2, "vending", "VendingMachine")
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