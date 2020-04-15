using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private ItemId id;

    public LayerMask LayerMask;

    [SerializeField]
    GameSprite spriteUp = default;
    [SerializeField]
    GameSprite spriteLeft = default;

    GameSprite currentSprite;

    public ItemDef Definition { get { return GlobalAccess.GetItemDefinitions().GetDefinition(id); } }

    GameSprite currentUser;

    public Collider2D Collider { get
        {
            return currentSprite?.Collider;
        }
    }

    public SpriteRenderer Renderer { get
        {
            return currentSprite.Renderer;
        }
    }

    public bool InUse { get { return currentUser != null; } }

    //ItemProperties properties;

    public System.Action OnCollisionChange;

    public virtual void Init(ItemId itemId)
    {
        id = itemId;

        spriteUp.gameObject.SetActive(false);
        spriteLeft.gameObject.SetActive(false);

        SetDirection(Direction.Up);
    }

    protected void Update()
    {
        if(InUse)
        {
            if (!currentSprite.IsCollidingWith(currentUser))
                currentUser = null;
        }
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
        currentUser = user.GetComponent<GameSprite>();

        return true;
    }

    public void SetDirection(Vector2 dir)
    {
        dir = dir.normalized;
        if (dir.x > 0.5f)
            SetDirection(Direction.Right);
        else if (dir.x < -0.5f)
            SetDirection(Direction.Left);
        else if (dir.y > 0.5f)
            SetDirection(Direction.Up);
        else if (dir.y < -0.5f)
            SetDirection(Direction.Down);
    }

    public void SetDirection(Direction direction)
    {
        if(currentSprite != null)
            currentSprite.gameObject.SetActive(false);

        if (direction == Direction.Up || direction == Direction.Down)
            currentSprite = spriteUp;
        else
            currentSprite = spriteLeft;

        currentSprite.gameObject.SetActive(true);
    }
}
