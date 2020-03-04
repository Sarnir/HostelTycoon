using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory
{
    List<Item> items;
    Dictionary<int, Bed> beds;

    public int CurrentSpace { get; private set; }

    public Inventory()
    {
        CurrentSpace = 0;

        items = new List<Item>();
        beds = new Dictionary<int, Bed>();
    }

    public void AddNewItem(Item newItem)
    {
        items.Add(newItem);
        CurrentSpace += newItem.Definition.TilesNeeded;

        if (newItem is Bed)
            AddBed(newItem as Bed);
    }

    public void RemoveItem(Item item)
    {
        CurrentSpace -= item.Definition.TilesNeeded;
        items.Remove(item);

        if (item is Bed)
            RemoveBed(item as Bed);
    }

    void AddBed(Bed newBed)
    {
        int bedsCount = beds.Count;
        for (int i = 0; i < bedsCount; i++)
        {
            if (beds[i] == null)
            {
                newBed.BedNo = i;
                beds[i] = newBed;
                return;
            }
        }

        newBed.BedNo = bedsCount;
        beds.Add(bedsCount, newBed);
    }

    void RemoveBed(Bed bed)
    {
        beds.Remove(bed.BedNo);
    }

    public Bed GetBed(int bedNo)
    {
        if (beds.ContainsKey(bedNo))
            return beds[bedNo];
        else
            return null;
    }

    public List<Bed> GetAllBeds()
    {
        return beds.Values.ToList();
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
