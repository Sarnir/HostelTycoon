using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Property
{
    // metraż
    // cena wynajmu/kupna ?
    // inventory

    int totalSpace = 30;

    Inventory inventory;

    public int TotalSpace
    {
        get { return totalSpace; }
    }

    public int AvailableSpace
    {
        get { return totalSpace - inventory.CurrentSpace; }
    }

    public int CurrentSpace
    {
        get { return inventory.CurrentSpace; }
    }

    public int FreeBedsCount
    {
        get { return inventory.GetAllBeds().FindAll(x => x.IsTaken == false).Count; }
    }

    public Property()
    {
        inventory = new Inventory();
    }

    public bool AddNewItem(Item newItem)
    {
        if (newItem.Definition.TilesNeeded <= AvailableSpace)
        {
            inventory.AddNewItem(newItem);
            return true;
        }

        return false;
    }

    public void RemoveItem(Item item)
    {
        inventory.RemoveItem(item);
    }

    public void ClearBed(int bedNo)
    {
        Bed bed = inventory.GetBed(bedNo);

        bed.Guest = null;
    }

    public Bed FindFreeBed()
    {
        return inventory.GetAllBeds().Find(x => x.IsTaken == false);
    }

    public Item[] GetAllItems()
    {
        return inventory.GetAllItems();
    }
}
