using UnityEngine;

public interface IItemDefinitions
{
    ItemDef GetDefinition(ItemId id);

    ItemDef[] GetAllDefinitions();
}