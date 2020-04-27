using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Left,
    Up,
    Right,
    Down
}

public class World : MonoBehaviour
{
    Tile[,] tiles;

    public int X_MAX = 27;
    public int Y_MAX = 20;

    [SerializeField]
    public ItemDef[] ItemDefs = null;

    public System.Action<Item> OnItemSpawned;

    public Transform SpawnPoint;

    public Tile FloorTilePrefab;

    public float AverageCleanliness { get; private set; }

    private void Awake()
    {
        SetupTiles();
    }

    private void SetupTiles()
    {
        tiles = new Tile[X_MAX, Y_MAX];

        var floorObj = new GameObject("Floor");
        floorObj.transform.parent = transform;

        // fill tiles array
        for (int y = 0; y < Y_MAX; y++)
        {
            for (int x = 0; x < X_MAX; x++)
            {
                var tile = Instantiate(FloorTilePrefab, floorObj.transform);
                tile.transform.position = new Vector3(x, y, 0);
                tiles[x, y] = tile;
            }
        }

        // setup neighbors
        for(int y = 0; y < Y_MAX; y++)
        {
            for (int x = 0; x < X_MAX; x++)
            {
                if (tiles[x, y] != null)
                {
                    if (y + 1 < Y_MAX && tiles[x, y + 1] != null)
                        tiles[x, y].SetNeighbor(Direction.Up, tiles[x, y + 1]);
                    if (y - 1 >= 0 && tiles[x, y - 1] != null)
                        tiles[x, y].SetNeighbor(Direction.Down, tiles[x, y - 1]);
                    if (x + 1 < X_MAX && tiles[x + 1, y] != null)
                        tiles[x, y].SetNeighbor(Direction.Right, tiles[x + 1, y]);
                    if (x - 1 >= 0 && tiles[x - 1, y] != null)
                        tiles[x, y].SetNeighbor(Direction.Left, tiles[x - 1, y]);
                }
            }
        }
    }

    public float dirtDelta = 0.0001f;

    private void Update()
    {
        float sum = 0f;
        int num = 0;

        /*foreach (var tile in tiles)
        {
            if (tile != null)
            {
                tile.IncrementDirt(dirtDelta * Time.deltaTime);
                sum += tile.Dirtyness;
                num++;
            }
        }*/

        float avgDirt = sum / num;

        AverageCleanliness = 1f - avgDirt;
    }

    public Tile GetTileAtPosition(int x, int y)
    {
        return tiles[x, y];
    }

    public Tile GetTileAtPosition(float x, float y)
    {
        return GetTileAtPosition(Mathf.RoundToInt(x), Mathf.RoundToInt(y));
    }

    public Item SpawnItem(ItemDef itemDef)
    {
        Item newItem = Instantiate(itemDef.Prefab);
        newItem.Init(itemDef.Id);
        DragItem(newItem);
        
        return newItem;
    }

    void PlaceItem(Hovering item, bool isSuccess)
    {
        if (isSuccess)
        {
            item.StopHovering();
            OnItemSpawned?.Invoke(item.GetComponent<Item>());
        }
        else
        {
            Destroy(item.gameObject);
        }
    }

    void DragItem(Item item)
    {
        var instance = item.gameObject.AddComponent<Hovering>();
        instance.transform.parent = transform;
        instance.OnPlaceItem += PlaceItem;
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
