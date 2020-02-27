using UnityEngine;

public interface IItemDefinitions
{
    ItemDef GetDefinition(int id);

    ItemDef[] GetAllDefinitions();
}