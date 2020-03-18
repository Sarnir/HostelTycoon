using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory
{
    List<Item> items;
    List<Bed> beds;

    public int CurrentSpace { get; private set; }

    public int FreeBedsCount
    {
        get { return beds.FindAll(x => x.IsTaken == false).Count; }
    }

    public Inventory()
    {
        CurrentSpace = 0;

        items = new List<Item>();
        beds = new List<Bed>();
    }

    public void AddNewItem(Item newItem)
    {
        items.Add(newItem);
        CurrentSpace += newItem.Definition.TilesNeeded;

        if (newItem is Bed)
            beds.Add(newItem as Bed);
    }

    public Bed FindFreeBed()
    {
        return beds.Find(x => x.IsTaken == false);
    }

    public Item[] GetAllItems()
    {
        return items.ToArray();
    }

    public Item FindItem(ItemId id)
    {
        return items.Find(item => item.Definition.Id == id);
    }

    public Item[] FindItems(ItemId id)
    {
        return items.FindAll(item => item.Definition.Id == id).ToArray();
    }
}
