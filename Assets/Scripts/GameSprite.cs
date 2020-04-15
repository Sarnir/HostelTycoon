using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSprite : MonoBehaviour
{
    [HideInInspector]
    public Collider2D Collider { get; private set; }
    [HideInInspector]
    public SpriteRenderer Renderer { get; private set; }

    void Awake()
    {
        Collider = GetComponent<Collider2D>();
        Renderer = GetComponent<SpriteRenderer>();
    }

    public bool IsCollidingWith(GameSprite other)
    {
        return Collider.IsTouching(other.Collider);
    }
}
