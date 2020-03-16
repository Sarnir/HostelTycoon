using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    [SerializeField]
    public ItemDef[] ItemDefs = null;

    Item draggingItem;

    [SerializeField]
    float hoverHeight = 0f;

    static float hoverOffset;

    public System.Action<Item> OnItemSpawned;

    bool IsDragging { get { return draggingItem != null; } }

    // Update is called once per frame
    void Update()
    {
        if(draggingItem != null)
        {
            draggingItem.transform.position = SnapPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition), IsDragging);

            if (Input.GetMouseButtonDown(0))
            {
                PlaceItem();
            }
        }
    }

    public static Vector3 SnapPosition(Vector3 pos, bool offsetY)
    {
        float x = Mathf.RoundToInt(pos.x);
        float y = Mathf.RoundToInt(pos.y);

        if (offsetY)
            y += hoverOffset;

        return new Vector3(x, y, 0);
    }

    void SnapPosition(Item item)
    {
        item.transform.position = SnapPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition), IsDragging);
    }

    public Item SpawnItem(ItemDef itemDef)
    {
        Item newItem = GameObject.Instantiate(itemDef.Prefab);
        newItem.Init(itemDef.Id);
        newItem.OnCollisionChange += DraggingCollisionChange;

        DragItem(newItem);
        draggingItem.transform.parent = transform;

        return newItem;
    }

    public PersonSprite SpawnPerson(Person person)
    {
        PersonSprite sprite = Instantiate(Resources.Load<PersonSprite>($"Prefabs/People/Lilly"));
        sprite.Init(person);

        return sprite;
    }

    void DraggingCollisionChange()
    {
        if(draggingItem)
        {
            SetItemState(draggingItem);
        }
    }

    void PlaceItem()
    {
        if (draggingItem.IsColliding)
        {
            Destroy(draggingItem.gameObject);
            draggingItem = null;
        }
        else
        {
            draggingItem.Renderer.color = new Color(1f, 1f, 1f, 1f);
            SnapPosition(draggingItem);
            OnItemSpawned?.Invoke(draggingItem);
        }

        hoverOffset = 0f;
        draggingItem = null;
    }

    void DragItem(Item item)
    {
        draggingItem = item;
        SetItemState(draggingItem);
        hoverOffset = hoverHeight;
    }

    void SetItemState(Item item)
    {
        if(item.IsColliding)
            item.Renderer.color = new Color(1f, 0f, 0f, 0.5f);
        else
            item.Renderer.color = new Color(0f, 1f, 0f, 0.5f);
    }
}
