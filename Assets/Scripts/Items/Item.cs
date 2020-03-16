using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private ItemId id;

    SpriteRenderer rd;
    public SpriteRenderer Renderer { get {
            if (rd == null) rd = GetComponent<SpriteRenderer>();
            return rd;
        } }

    public ItemDef Definition { get { return GlobalAccess.GetItemDefinitions().GetDefinition(id); } }

    Collider2D col;

    bool isColliding;
    public bool IsColliding { get { return isColliding; } }

    //ItemProperties properties;

    public System.Action OnCollisionChange;

    public void Start()
    {
        isColliding = false;
        col = GetComponent<Collider2D>();
    }

    public virtual void Init(ItemId itemId)
    {
        id = itemId;
    }

    private void OnDrawGizmos()
    {
        if(col != null)
            Gizmos.DrawWireCube(col.bounds.center, col.bounds.size);
    }

    void OnTriggerEnter2D(Collider2D col)
     {
         isColliding = true;
         OnCollisionChange?.Invoke();
     }

     void OnTriggerExit2D(Collider2D col)
     {
         isColliding = false;
         OnCollisionChange?.Invoke();
     }

    // zazwyczaj to goście lub staff będą korzystać z rzeczy
    // to oni będą wywoływać tę metodę i np. zostawiać piniondze w automacie
    // lub pójdą spać i dostaną pluskwy xD
    public virtual bool Use(Person user)
    {
        return true;
    }
}
