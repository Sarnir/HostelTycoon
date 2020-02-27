using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    // dwa rodzaje łóżek
    // stół bilardowy
    // lodówka z napojami

    private int id;

    public ItemDef Definition { get { return GlobalAccess.GetItemDefinitions().GetDefinition(id); } }

    Vector2 position; // gdzie stoi na mapce

    public Item(int itemId)
    {
        id = itemId;
    }

    // zazwyczaj to goście lub staff będą korzystać z rzeczy
    // to oni będą wywoływać tę metodę i np. zostawiać piniondze w automacie
    // lub pójdą spać i dostaną pluskwy xD
    public virtual void Interact()
    {

    }
}
