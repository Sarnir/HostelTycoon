using UnityEngine;

public class ItemProperty
{
    public HostelQuality Quality { get; private set; }
    public int Factor { get; private set; }

public ItemProperty(HostelQuality quality, int factor)
    {
        Quality = quality;
        Factor = factor;
    }
}

[System.Serializable]
public class ItemDef
{
    [SerializeField]
    private ItemId id;
    public ItemId Id { get { return id; } }

    [SerializeField]
    private string name;
    public string Name { get { return name; } }

    [SerializeField]
    private int price;
    public int Price { get { return price; } }
    public Sprite Avatar { get; private set; }
    public int TilesNeeded { get; private set; }
    public ItemProperty[] ItemProperties { get; private set; }

    [SerializeField]
    Item prefab;

    public Item Prefab { get { return prefab; } }

    public ItemDef(ItemId _id, string _name, int _price, int _tiles, string _avatarPath, string _prefabPath, ItemProperty[] _properties = null)
    {
        id = _id;
        name = _name;
        price = _price;
        TilesNeeded = _tiles;
        Avatar = Resources.Load<Sprite>("Avatars/items/" + _avatarPath);
        ItemProperties = _properties;

        prefab = Resources.Load<Item>($"Prefabs/Objects/{ _prefabPath }");
    }
}