using UnityEngine;

public class ItemDef
{
    public ItemId Id { get; private set; }
    public string Name { get; private set; }
    public int Price { get; private set; }
    public Sprite Avatar { get; private set; }
    public int TilesNeeded { get; private set; }

    // typ przedmiotu

    public ItemDef(ItemId id, string name, int price, int tiles, string avatarPath)
    {
        Id = id;
        Name = name;
        Price = price;
        TilesNeeded = tiles;
        Avatar = Resources.Load<Sprite>("Avatars/items/" + avatarPath);
    }

    public Item CreateInstance()
    {
        Item newInstance = null;

        switch (Id)
        {
            case ItemId.CheapBed:
            case ItemId.LuxuryBed:
            case ItemId.BunkBed:
                newInstance = new Bed(Id);
                break;
            case ItemId.VendingMachine:
                newInstance = new VendingMachine(Id);
                break;
            default:
                newInstance = new Item(Id);
                break;
        }

        return newInstance;
    }
}