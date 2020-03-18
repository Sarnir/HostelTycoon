using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    [SerializeField]
    public ItemDef[] ItemDefs = null;

    Hovering hoveringItem;

    public System.Action<Item> OnItemSpawned;

    bool IsDragging { get { return hoveringItem != null; } }

    void Update()
    {
        if (IsDragging)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PlaceItem();
            }
        }
    }

    public Item SpawnItem(ItemDef itemDef)
    {
        Item newItem = GameObject.Instantiate(itemDef.Prefab);
        newItem.Init(itemDef.Id);
        //newItem.OnCollisionChange += DraggingCollisionChange;

        DragItem(newItem);
        hoveringItem.transform.parent = transform;

        return newItem;
    }

    void PlaceItem()
    {
        if (hoveringItem.IsColliding)
        {
            Destroy(hoveringItem.gameObject);
        }
        else
        {
            hoveringItem.StopHovering();
            OnItemSpawned?.Invoke(hoveringItem.GetComponent<Item>());
        }

        hoveringItem = null;
    }

    void DragItem(Item item)
    {
        hoveringItem = item.gameObject.AddComponent<Hovering>();
    }

    public Guest CreateGuest(Hostel hostel, int lengthOfStay)
    {
        return CreateGuest(hostel, lengthOfStay, new PersonData());
    }

    public Guest CreateGuest(Hostel hostel, int lengthOfStay, PersonData pData)
    {
        Guest sprite = Instantiate(Resources.Load<Guest>($"Prefabs/People/Guest"));
        sprite.Init(hostel, pData);
        sprite.LengthOfStay = lengthOfStay;

        return sprite;
    }

    public Employee CreateEmployee(Hostel hostel, PersonData pData)
    {
        Employee sprite = Instantiate(Resources.Load<Employee>("Prefabs/People/Employee"));
        sprite.Init(hostel, pData);

        return sprite;
    }
}
