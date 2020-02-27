using UnityEngine;

public enum ItemType
{
    Generic,
    Bed
}

public class ItemDef
{
    public int Id { get; private set; }
    public ItemType Type { get; private set; }
    public string Name { get; private set; }
    public int Price { get; private set; }
    public Sprite Avatar { get; private set; }
    public int TilesNeeded { get; private set; }

    // typ przedmiotu

    public ItemDef(int id, ItemType type, string name, int price, int tiles, string avatarPath)
    {
        Id = id;
        Type = type;
        Name = name;
        Price = price;
        TilesNeeded = tiles;
        Avatar = Resources.Load<Sprite>("Avatars/items/" + avatarPath);
    }

    public Item CreateInstance()
    {
        Item newInstance = null;

        switch (Type)
        {
            case ItemType.Generic:
                newInstance = new Item(Id);
                break;
            case ItemType.Bed:
                newInstance = new Bed(Id);
                break;
        }
        Bed bed = new Bed(0);

        return newInstance;
    }
}