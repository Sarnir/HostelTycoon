using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hovering : MonoBehaviour
{
    Item item;
    ContactFilter2D filter;

    Vector3 lastPos;
    bool posChanged;

    const float hoverHeight = 0.2f;
    Rigidbody2D rb;

    public bool IsColliding { get; private set; }

    void Start()
    {
        item = GetComponent<Item>();
        item.Renderer.transform.localPosition = new Vector3(0f, hoverHeight, 0f);
        filter = new ContactFilter2D() { layerMask = item.LayerMask, useTriggers = true, useLayerMask = true };
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.isKinematic = true;
    }

    void FixedUpdate()
    {
        if (posChanged)
        {
            RaycastHit2D[] hits = new RaycastHit2D[1];
            IsColliding = item.Collider.Cast(Vector2.zero, filter, hits) > 0;

            if (IsColliding)
                item.Renderer.color = new Color(1f, 0f, 0f, 0.5f);
            else
                item.Renderer.color = new Color(0f, 1f, 0f, 0.5f);
        }

        lastPos = transform.position;

        transform.localPosition = SnapPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        posChanged = lastPos != transform.position;
    }

    Vector3 SnapPosition(Vector3 pos)
    {
        float x = Mathf.RoundToInt(pos.x);
        float y = Mathf.RoundToInt(pos.y);

        return new Vector3(x, y, 0);
    }

    public void StopHovering()
    {
        item.Renderer.transform.localPosition = new Vector3(0f, 0f, 0f);
        item.Renderer.color = new Color(1f, 1f, 1f, 1f);

        Destroy(rb);
        Destroy(this);
    }
}
