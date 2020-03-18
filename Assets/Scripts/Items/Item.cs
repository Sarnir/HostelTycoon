using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private ItemId id;

    public LayerMask LayerMask;

    public SpriteRenderer Renderer { get; private set; }

    public ItemDef Definition { get { return GlobalAccess.GetItemDefinitions().GetDefinition(id); } }

    public Collider2D Collider { get; private set; }

    public bool InUse { get; private set; }

    //ItemProperties properties;

    public System.Action OnCollisionChange;

    public void Awake()
    {
        Collider = GetComponent<Collider2D>();
        Renderer = GetComponentInChildren<SpriteRenderer>();
    }

    public virtual void Init(ItemId itemId)
    {
        id = itemId;
    }

    private void OnDrawGizmos()
    {
        if(Collider != null)
            Gizmos.DrawWireCube(Collider.bounds.center, Collider.bounds.size);
    }


    // zazwyczaj to goście lub staff będą korzystać z rzeczy
    // to oni będą wywoływać tę metodę i np. zostawiać piniondze w automacie
    // lub pójdą spać i dostaną pluskwy xD
    public virtual bool Use(Person user)
    {
        InUse = true;

        return true;
    }
}
