﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Property
{
    // metraż
    // cena wynajmu/kupna ?
    // inventory

    public World World;

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

    public Property(World _world)
    {
        inventory = new Inventory();
        World = _world;

        World.OnItemSpawned += ItemSpawned;
    }

    void ItemSpawned(Item item)
    {
        inventory.AddNewItem(item);
    }

    public void AddNewItem(ItemDef def)
    {
        Item newItem = World.SpawnItem(def);

       /* if(newItem.Definition.Id == ItemId.SpaceExtension)
        {
            totalSpace++;
            return true;
        }

        // czekaj na callback po postawieniu itemu

        if (newItem.Definition.TilesNeeded <= AvailableSpace)
        {
            inventory.AddNewItem(newItem);
            return true;
        }

        return false;*/
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

    public Item FindItem(ItemId id)
    {
        return inventory.FindItem(id);
    }

    public Item[] FindItems(ItemId id)
    {
        return inventory.FindItems(id);
    }
}
