using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tile : MonoBehaviour
{
    static readonly float MAX_DIRT = 2f;
    public Tile[] Neighbors;
    SpriteRenderer sr;
    public float Dirtyness { get; private set; }

    int collisionsNum;
    public bool IsEmpty { get { return collisionsNum == 0; } }

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        Neighbors = new Tile[4];
    }

    public void SetNeighbor(Direction direction, Tile neighbor)
    {
        Neighbors[(int)direction] = neighbor;
    }

    public void IncrementDirt(float delta)
    {
        UpdateDirt(Dirtyness + delta);
    }

    public void UpdateDirt(float value)
    {
        Dirtyness = Mathf.Clamp01(value);

        sr.material.SetFloat("_Strength", Dirtyness * MAX_DIRT);

        foreach (var nb in Neighbors)
        {
            if(nb != null)
                nb.UpdateNeighborsDirtyness();
        }
    }

    public void UpdateNeighborsDirtyness()
    {
        Vector4 neighborsDirtyness = new Vector4()
        {
            x = Neighbors[0] != null ? Neighbors[0].Dirtyness : Dirtyness,
            y = Neighbors[1] != null ? Neighbors[1].Dirtyness : Dirtyness,
            z = Neighbors[2] != null ? Neighbors[2].Dirtyness : Dirtyness,
            w = Neighbors[3] != null ? Neighbors[3].Dirtyness : Dirtyness
        };

        sr.material.SetVector("_Neighbors", neighborsDirtyness);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        IncrementDirt(0.02f);
        collisionsNum++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collisionsNum--;
    }

}
