using UnityEngine;

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
            new ItemDef(0, ItemType.Bed, "Cheap Bed", 400, 6, "bedCheap"),
            new ItemDef(1, ItemType.Bed, "Luxury Bed", 800, 6, "bedLuxury"),
            new ItemDef(2, ItemType.Bed, "Bunk Bed", 600, 6, "bedBunk"),
            new ItemDef(3, ItemType.Generic ,"Pool Table", 1000, 6, "poolTable"),
            new ItemDef(4, ItemType.Generic, "Vending Machine", 1200, 2, "vending")
        };
    }

    public ItemDef GetDefinition(int id)
    {
        return definitions[id];
    }

    public ItemDef[] GetAllDefinitions()
    {
        return definitions.Clone() as ItemDef[];
    }
}