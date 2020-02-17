using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Property
{
    // metraż
    // cena wynajmu/kupna ?
    // inventory

    int bedLimit = 10;

    List<Bed> beds;

    public int BedLimit
    {
        get { return bedLimit; }
    }

    public int BedQuantity
    {
        get { return beds.Count; }
    }

    public Property()
    {
        beds = new List<Bed>(4)
        {
            new Bed(),
            new Bed(),
            new Bed(),
            new Bed()
        };
    }

    public List<Bed> GetBeds()
    {
        return beds;
    }

    public bool AddNewBed()
    {
        if (BedQuantity < bedLimit)
        {
            beds.Add(new Bed());
            return true;
        }

        return false;
    }

    public void ClearBed(Guest guest)
    {
        var bed = beds.Find(x => x.Guest == guest);
        bed.Guest = null;
    }
}
